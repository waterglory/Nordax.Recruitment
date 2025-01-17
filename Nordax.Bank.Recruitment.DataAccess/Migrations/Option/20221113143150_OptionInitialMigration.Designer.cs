﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nordax.Bank.Recruitment.DataAccess.DbContexts;

#nullable disable

namespace Nordax.Bank.Recruitment.DataAccess.Migrations.Option
{
    [DbContext(typeof(OptionDbContext))]
    [Migration("20221113143150_OptionInitialMigration")]
    partial class OptionInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.Option.BindingPeriod", b =>
                {
                    b.Property<int>("Length")
                        .HasPrecision(3)
                        .HasColumnType("int");

                    b.Property<decimal>("InterestRate")
                        .HasPrecision(8, 5)
                        .HasColumnType("decimal(8,5)");

                    b.HasKey("Length");

                    b.ToTable("BindingPeriods");
                });
#pragma warning restore 612, 618
        }
    }
}
