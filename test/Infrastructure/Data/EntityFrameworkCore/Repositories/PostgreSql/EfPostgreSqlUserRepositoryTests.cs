﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Infrastructure.EntityFrameworkCore.PostgreSQL;
using SharedKernel.Integration.Tests.Data.CommonRepositoryTesting;
using Xunit;

namespace SharedKernel.Integration.Tests.Data.EntityFrameworkCore.Repositories.PostgreSql;

[Collection("DockerHook")]
public class EfPostgreSqlUserRepositoryTests : UserRepositoryCommonTestTests<EfCoreUserRepository>
{
    protected override string GetJsonFile()
    {
        return "Data/EntityFrameworkCore/Repositories/PostgreSql/appsettings.postgreSql.json";
    }

    protected override void Regenerate()
    {
        var dbContext = GetRequiredService<PostgreSqlSharedKernelDbContext>();
        //dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();
    }

    protected override IServiceCollection ConfigureServices(IServiceCollection services)
    {
        var connection = Configuration.GetConnectionString("RepositoryConnectionString")!;
        return services
            .AddEntityFrameworkCorePostgreSqlUnitOfWorkAsync<ISharedKernelUnitOfWork, PostgreSqlSharedKernelDbContext>(connection)
            .AddTransient<EfCoreUserRepository>();
    }
}
