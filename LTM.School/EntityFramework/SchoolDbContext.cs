using LTM.School.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.EntityFramework
{
  public class SchoolDbContext : DbContext
  {
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {

    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<Instructor> Instructors { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<CourseAssignment> CourseAssignments { get; set; }

    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<Student>().ToTable("Student");

      modelBuilder.Entity<Course>().ToTable("Course").Property(c => c.Id).ValueGeneratedNever();

      modelBuilder.Entity<Enrollment>().ToTable("Enrollment");

      modelBuilder.Entity<Instructor>().ToTable("Instructor");

      modelBuilder.Entity<Department>().ToTable("Department");

      modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment").HasKey(c => new { c.CourseId, c.InstructorId });

      modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");

    }
  }
}