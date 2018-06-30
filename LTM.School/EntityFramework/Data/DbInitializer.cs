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

      #region 添加种子学生信息
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
      #endregion

      #region 添加种子老师信息

      var instructors = new Instructor[] {
        new Instructor{ RealName = "孔子", HireDate = DateTime.Parse("1993-08-12") },
        new Instructor{ RealName = "墨子", HireDate = DateTime.Parse("1994-08-12") },
        new Instructor{ RealName = "荀子", HireDate = DateTime.Parse("1995-08-12") },
        new Instructor{ RealName = "鬼谷子", HireDate = DateTime.Parse("1996-08-12") },
        new Instructor{ RealName = "孟子", HireDate = DateTime.Parse("1997-08-12") },
        new Instructor{ RealName = "朱熹", HireDate = DateTime.Parse("1998-08-12") },
        new Instructor{ RealName = "庄子", HireDate = DateTime.Parse("1999-08-12") },
      };

      foreach(var i in instructors)
      {
        context.Instructors.Add(i);
      }
      context.SaveChanges();

      #endregion

      #region 添加部门的种子数据

      var departments = new Department[]
      {
        new Department{ Name = "声乐", Budget = 350000, StartDate = DateTime.Parse("2013-09-04"),  InstructorId = instructors.Single(i=>i.RealName == "墨子").Id},
        new Department{ Name = "杂技", Budget = 350000, StartDate = DateTime.Parse("2014-09-04") , InstructorId = instructors.Single(i=>i.RealName == "孔子").Id},
        new Department{ Name = "学说", Budget = 350000, StartDate = DateTime.Parse("2015-09-04") , InstructorId = instructors.Single(i=>i.RealName == "庄子").Id},
        new Department{ Name = "兵法", Budget = 350000, StartDate = DateTime.Parse("2016-09-04"), InstructorId = instructors.Single(i=>i.RealName == "荀子").Id }
      };

      foreach (var d in departments)
      {
        context.Departments.Add(d);
      }
      context.SaveChanges();
      #endregion

      #region 添加种子课程

      var courses = new Course[] {
        new Course{ Id = 10010, Title = "政治",  Credits = 6, DepartmentId = departments.Single(d=>d.Name == "声乐").Id },
        new Course{ Id = 10011, Title = "历史",  Credits = 4, DepartmentId = departments.Single(d=>d.Name == "杂技").Id },
        new Course{ Id = 10012, Title = "语文",  Credits = 4, DepartmentId = departments.Single(d=>d.Name == "杂技").Id },
        new Course{ Id = 10013, Title = "地理",  Credits = 4, DepartmentId = departments.Single(d=>d.Name == "兵法").Id },
      };

      foreach (var course in courses)
      {
        context.Courses.Add(course);
      }
      context.SaveChanges();
      #endregion

      #region 添加办公室分配的种子数据

      var officeAssignments = new OfficeAssignment[]
      {
        new OfficeAssignment{ InstructorId = instructors.Single(i=>i.RealName == "墨子").Id, Location = "逸夫楼17"  },
        new OfficeAssignment{ InstructorId = instructors.Single(i=>i.RealName == "孔子").Id, Location = "逸夫楼18"  },
        new OfficeAssignment{ InstructorId = instructors.Single(i=>i.RealName == "庄子").Id, Location = "逸夫楼19"  },
      };

      foreach (var o in officeAssignments)
      {
        context.OfficeAssignments.Add(o);
      }
      context.SaveChanges();
      #endregion

      #region 添加课程老师的种子数据

      var courseInstrutors = new CourseAssignment[] 
      {
        new CourseAssignment{ CourseId = courses.Single(c=>c.Title == "政治").Id, InstructorId = instructors.Single(i=>i.RealName == "孟子").Id },
        new CourseAssignment{ CourseId = courses.Single(c=>c.Title == "政治").Id, InstructorId = instructors.Single(i=>i.RealName == "孔子").Id },
        new CourseAssignment{ CourseId = courses.Single(c=>c.Title == "语文").Id, InstructorId = instructors.Single(i=>i.RealName == "庄子").Id },
        new CourseAssignment{ CourseId = courses.Single(c=>c.Title == "历史").Id, InstructorId = instructors.Single(i=>i.RealName == "庄子").Id },
      };

      foreach (var c in courseInstrutors)
      {
        context.CourseAssignments.Add(c);
      }
      context.SaveChanges();
      #endregion

      #region 添加招生信息种子数据

      var enrollments = new Enrollment[]
      {
        new Enrollment { StudentId = students.Single(s=>s.RealName == "乔峰").Id, CourseId = courses.Single(c=>c.Title == "政治").Id, Grade = CourseGrade.A},
        new Enrollment { StudentId = students.Single(s=>s.RealName == "乔峰").Id, CourseId = courses.Single(c=>c.Title == "历史").Id, Grade = CourseGrade.A},
        new Enrollment { StudentId = students.Single(s=>s.RealName == "段誉").Id, CourseId = courses.Single(c=>c.Title == "语文").Id, Grade = CourseGrade.B},
        new Enrollment { StudentId = students.Single(s=>s.RealName == "虚竹").Id, CourseId = courses.Single(c=>c.Title == "地理").Id, Grade = CourseGrade.C},
      };
      
      foreach(var e in enrollments)
      {
        context.Enrollments.Add(e);
      }
      context.SaveChanges();

      #endregion

    }
  }
}
