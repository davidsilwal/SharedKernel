﻿// <auto-generated />
using System;
using BankAccounts.Infrastructure.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankAccounts.Infrastructure.Shared.Data.Migrations
{
    [DbContext(typeof(BankAccountDbContext))]
    partial class BankAccountDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankAccounts.Domain.BankAccounts.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("BankAccount", "dbo");
                });

            modelBuilder.Entity("BankAccounts.Domain.BankAccounts.Movement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Concept")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.ToTable("Movement", "dbo");
                });

            modelBuilder.Entity("BankAccounts.Domain.BankAccounts.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("Emails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Surname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("User", "dbo");
                });

            modelBuilder.Entity("SharedKernel.Infrastructure.Requests.Middlewares.Failover.ErrorRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Exception")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurredOn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Request")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ErrorRequest", "dbo");
                });

            modelBuilder.Entity("BankAccounts.Domain.BankAccounts.BankAccount", b =>
                {
                    b.HasOne("BankAccounts.Domain.BankAccounts.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.OwnsOne("BankAccounts.Domain.BankAccounts.InternationalBankAccountNumber", "InternationalBankAccountNumber", b1 =>
                        {
                            b1.Property<Guid>("BankAccountId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AccountNumber")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)")
                                .HasColumnName("Number");

                            b1.Property<string>("ControlDigit")
                                .HasMaxLength(2)
                                .HasColumnType("nvarchar(2)");

                            b1.Property<string>("CountryCheckDigit")
                                .HasMaxLength(4)
                                .HasColumnType("nvarchar(4)");

                            b1.Property<string>("EntityCode")
                                .HasMaxLength(4)
                                .HasColumnType("nvarchar(4)");

                            b1.Property<string>("OfficeNumber")
                                .HasMaxLength(4)
                                .HasColumnType("nvarchar(4)");

                            b1.HasKey("BankAccountId");

                            b1.ToTable("BankAccount", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("BankAccountId");
                        });

                    b.Navigation("InternationalBankAccountNumber");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BankAccounts.Domain.BankAccounts.Movement", b =>
                {
                    b.HasOne("BankAccounts.Domain.BankAccounts.BankAccount", null)
                        .WithMany("Movements")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BankAccount");
                });

            modelBuilder.Entity("BankAccounts.Domain.BankAccounts.BankAccount", b =>
                {
                    b.Navigation("Movements");
                });
#pragma warning restore 612, 618
        }
    }
}
