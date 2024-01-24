﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using W8_Backend.Data;

#nullable disable

namespace W8_Backend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221006060123_addedFieldsInSysVar")]
    partial class addedFieldsInSysVar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("W8_Backend.Data.Entities.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyID"), 1L, 1);

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NavConnectionString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("W8_Backend.Data.Entities.EmployeeMonthlyWorkDifferences", b =>
                {
                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<string>("Emp_number")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeDesc")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("CostPerMonth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsJob")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSynced")
                        .HasColumnType("bit");

                    b.Property<decimal>("Percent_of_month")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Percent_working")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total_minus_month")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Working_hours")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Month", "Year", "Emp_number", "CodeNumber", "CodeDesc", "CompanyName");

                    b.ToTable("EmployeeMonthlyWorkDifferences");
                });

            modelBuilder.Entity("W8_Backend.Data.Entities.LogDetails", b =>
                {
                    b.Property<int>("DetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailID"), 1L, 1);

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LogDetailTimeStamp")
                        .HasColumnType("datetime");

                    b.Property<int>("LogID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetailID");

                    b.HasIndex("LogID");

                    b.ToTable("LogDetails");
                });

            modelBuilder.Entity("W8_Backend.Data.Entities.Logs", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogID"), 1L, 1);

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LogEndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("W8_Backend.Data.Entities.SystemVariables", b =>
                {
                    b.Property<int>("VariableID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VariableID"), 1L, 1);

                    b.Property<string>("Api1Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Api2Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HRCompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HRJobNb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HRTaskNb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSyncing")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastSyncDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LogCleanDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LogCleanRuntime")
                        .HasColumnType("datetime");

                    b.Property<string>("MonthlyCostSheetPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Retention")
                        .HasColumnType("int");

                    b.Property<bool>("SyncEmpDiff")
                        .HasColumnType("bit");

                    b.Property<bool>("SyncMonthlyCostSheet")
                        .HasColumnType("bit");

                    b.Property<bool>("SyncStatus")
                        .HasColumnType("bit");

                    b.Property<string>("TargetPathForMonthlyCostSheet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VariableID");

                    b.ToTable("SystemVariables");
                });

            modelBuilder.Entity("W8_Backend.Data.Entities.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MicroUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserID");

                    b.HasIndex("MicroUserID")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("W8_Backend.Data.Entities.LogDetails", b =>
                {
                    b.HasOne("W8_Backend.Data.Entities.Logs", "Log")
                        .WithMany("LogDetails")
                        .HasForeignKey("LogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Log");
                });

            modelBuilder.Entity("W8_Backend.Data.Entities.Logs", b =>
                {
                    b.Navigation("LogDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
