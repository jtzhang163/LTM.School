using LTM.School.Application.enumsType;
using LTM.School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.EntityFramework.Data
{
  public class DbInitialiser
  {
    public static void Initialise(SchoolDbContext context)
    {
      context.Database.EnsureCreated();//要创建数据库

      if (context.Students.Any())
      {
        return;
      }

      var students = new Student[] {
        new Student{  RealName = "乔峰", EnrollmnetDate = DateTime.Parse("2011-08-26") },
        new Student{  RealName = "段誉", EnrollmnetDate = DateTime.Parse("2012-08-26") },
        new Student{  RealName = "虚竹", EnrollmnetDate = DateTime.Parse("2011-08-26") },
        new Student{  RealName = "令狐冲", EnrollmnetDate = DateTime.Parse("2013-07-26") },
        new Student{  RealName = "张无忌", EnrollmnetDate = DateTime.Parse("2015-08-26") },
        new Student{  RealName = "杨过", EnrollmnetDate = DateTime.Parse("2014-08-26") },
        new Student{  RealName = "黄蓉", EnrollmnetDate = DateTime.Parse("2011-08-26") },
      };

      foreach(var student in students)
      {
        context.Students.Add(student);
      }
      context.SaveChanges();

      var courses = new Course[] {
        new Course{ CourseId = 10010, Title = "政治",  Credits = 6 },
        new Course{ CourseId = 10011, Title = "历史",  Credits = 4 },
      };

      foreach (var course in courses)
      {
        context.Courses.Add(course);
      }
      context.SaveChanges();

      var enrollments = new Enrollment[] {
        new Enrollment{  StudentId =1, CourseId = 10010, Grade = CourseGrade.A },
        new Enrollment{  StudentId =2, CourseId = 10011, Grade = CourseGrade.B },
      };

      foreach (var enrollment in enrollments)
      {
        context.Enrollments.Add(enrollment);
      }
      context.SaveChanges();

    }
  }
}
