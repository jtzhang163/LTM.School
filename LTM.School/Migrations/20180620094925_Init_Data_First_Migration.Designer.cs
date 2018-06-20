﻿// <auto-generated />
using LTM.School.Application.enumsType;
using LTM.School.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LTM.School.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20180620094925_Init_Data_First_Migration")]
    partial class Init_Data_First_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LTM.School.Core.Models.Course", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("Credits");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("LTM.School.Core.Models.CourseAssignment", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("InstructorId");

                    b.HasKey("CourseId", "InstructorId");

                    b.HasIndex("InstructorId");

                    b.ToTable("CourseAssignment");
                });

            modelBuilder.Entity("LTM.School.Core.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<int?>("InstructorId");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("LTM.School.Core.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int?>("Grade");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("LTM.School.Core.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("RealName");

                    b.HasKey("Id");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("LTM.School.Core.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorId");

                    b.Property<string>("Location")
                        .HasMaxLength(50);

                    b.HasKey("InstructorId");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("LTM.School.Core.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EnrollmnetDate");

                    b.Property<string>("RealName")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<string>("Secret")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("LTM.School.Core.Models.Course", b =>
                {
                    b.HasOne("LTM.School.Core.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LTM.School.Core.Models.CourseAssignment", b =>
                {
                    b.HasOne("LTM.School.Core.Models.Course", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LTM.School.Core.Models.Instructor", "Instructor")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LTM.School.Core.Models.Department", b =>
                {
                    b.HasOne("LTM.School.Core.Models.Instructor", "Administrator")
                        .WithMany()
                        .HasForeignKey("InstructorId");
                });

            modelBuilder.Entity("LTM.School.Core.Models.Enrollment", b =>
                {
                    b.HasOne("LTM.School.Core.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LTM.School.Core.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LTM.School.Core.Models.OfficeAssignment", b =>
                {
                    b.HasOne("LTM.School.Core.Models.Instructor", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("LTM.School.Core.Models.OfficeAssignment", "InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
