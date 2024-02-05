﻿// <auto-generated />
using System;
using Bulk_Email_Sending_Groupwise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bulk_Email_Sending_Groupwise.Migrations
{
    [DbContext(typeof(BulkDbContext))]
    [Migration("20240205090435_second")]
    partial class second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.Department", b =>
                {
                    b.Property<int>("Dept_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Dept_Id"));

                    b.Property<string>("Dept_Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Dept_Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.EmpDeptMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Dept_Id")
                        .HasColumnType("int");

                    b.Property<int>("Emp_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Dept_Id");

                    b.HasIndex("Emp_ID");

                    b.ToTable("EmpDeptMapping");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.EmpMenuMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<int>("MenuID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpId");

                    b.HasIndex("MenuID");

                    b.ToTable("EmpMenuMapping");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.Employee", b =>
                {
                    b.Property<int>("Emp_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Emp_ID"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Emp_ID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.Menus", b =>
                {
                    b.Property<int>("MenuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuID"));

                    b.Property<string>("MenuName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.HasKey("MenuID");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.EmpDeptMapping", b =>
                {
                    b.HasOne("Bulk_Email_Sending_Groupwise.Models.Department", "Department")
                        .WithMany("EmpDeptMapping")
                        .HasForeignKey("Dept_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bulk_Email_Sending_Groupwise.Models.Employee", "Employee")
                        .WithMany("EmpDeptMapping")
                        .HasForeignKey("Emp_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.EmpMenuMapping", b =>
                {
                    b.HasOne("Bulk_Email_Sending_Groupwise.Models.Employee", "Employee")
                        .WithMany("EmpMenuMapping")
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bulk_Email_Sending_Groupwise.Models.Menus", "Menus")
                        .WithMany("EmpMenuMapping")
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Menus");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.Department", b =>
                {
                    b.Navigation("EmpDeptMapping");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.Employee", b =>
                {
                    b.Navigation("EmpDeptMapping");

                    b.Navigation("EmpMenuMapping");
                });

            modelBuilder.Entity("Bulk_Email_Sending_Groupwise.Models.Menus", b =>
                {
                    b.Navigation("EmpMenuMapping");
                });
#pragma warning restore 612, 618
        }
    }
}
