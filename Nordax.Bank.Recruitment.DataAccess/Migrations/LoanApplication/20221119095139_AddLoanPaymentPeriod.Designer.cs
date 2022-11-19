﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;

#nullable disable

namespace Nordax.Bank.Recruitment.DataAccess.Migrations.LoanApplication
{
    [DbContext(typeof(LoanApplicationDbContext))]
    [Migration("20221119095139_AddLoanPaymentPeriod")]
    partial class AddLoanPaymentPeriod
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.Applicant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IncomeLevel")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsPoliticallyExposed")
                        .HasColumnType("bit");

                    b.Property<string>("OrganizationNo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("FileRef")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("LoanApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LoanApplicationId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("BindingPeriod")
                        .HasPrecision(3)
                        .HasColumnType("int");

                    b.Property<decimal>("InterestRate")
                        .HasPrecision(8, 5)
                        .HasColumnType("decimal(8,5)");

                    b.Property<int>("PaymentPeriod")
                        .HasPrecision(4)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.LoanApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CaseNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentStep")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("LoanId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("CaseNo")
                        .IsUnique();

                    b.HasIndex("LoanId");

                    b.ToTable("LoanApplications");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.Document", b =>
                {
                    b.HasOne("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.LoanApplication", null)
                        .WithMany("Documents")
                        .HasForeignKey("LoanApplicationId");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.LoanApplication", b =>
                {
                    b.HasOne("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.Applicant", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.Loan", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication.LoanApplication", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
