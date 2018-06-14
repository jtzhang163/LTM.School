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
    [Migration("20180614151336_Add_Migration_Student_MaxName")]
    partial class Add_Migration_Student_MaxName
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

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Course");
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

            modelBuilder.Entity("LTM.School.Core.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EnrollmnetDate");

                    b.Property<string>("RealName")
                        .HasMaxLength(8);

                    b.HasKey("Id");

                    b.ToTable("Student");
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
#pragma warning restore 612, 618
        }
    }
}
