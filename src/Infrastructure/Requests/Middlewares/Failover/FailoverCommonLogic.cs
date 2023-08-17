﻿using SharedKernel.Application.Serializers;
using SharedKernel.Application.System;
using SharedKernel.Domain.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharedKernel.Infrastructure.Requests.Middlewares.Failover;

/// <summary>  </summary>
public class FailoverCommonLogic
{
    private readonly IRequestFailoverRepository _requestFailOverRepository;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IRequestSerializer _requestSerializer;
    private readonly IGuid _guid;

    /// <summary>  </summary>
    public FailoverCommonLogic(
        IRequestFailoverRepository requestFailOverRepository,
        IJsonSerializer jsonSerializer,
        IRequestSerializer requestSerializer,
        IGuid guid)
    {
        _requestFailOverRepository = requestFailOverRepository;
        _jsonSerializer = jsonSerializer;
        _requestSerializer = requestSerializer;
        _guid = guid;
    }

    /// <summary>  </summary>
    public async Task Handle<TRequest>(TRequest request, Exception e, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        var error = new Dictionary<string, string>
        {
            {"Type", e.GetType().ToString()},
            {"Message", e.Message},
            {"StackTrace", e.StackTrace}
        };

        foreach (DictionaryEntry data in e.Data)
            error.Add(data.Key.ToString()!, data.Value?.ToString());

        var exceptionString = _jsonSerializer.Serialize(error);

        ErrorRequest errorRequest;
        if (request is Request requestTyped)
        {
            var requestString = _requestSerializer.Serialize(requestTyped);
            errorRequest = ErrorRequest.Create(requestTyped.RequestId, requestString, exceptionString,
                requestTyped.OccurredOn);
        }
        else
        {
            var requestString = _jsonSerializer.Serialize(request);
            errorRequest = ErrorRequest.Create(_guid.NewGuid().ToString(), requestString, exceptionString);
        }

        await _requestFailOverRepository.Save(errorRequest, cancellationToken);
    }
}
