using LTM.School.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.ViewModels
{
  public class InstructorIndexData
  {
    /// <summary>
    /// 老师的导航属性
    /// </summary>
    public IEnumerable<Instructor> Instructors { get; set; }

    public IEnumerable<Course> Courses { get; set; }

    public IEnumerable<Enrollment> Enrollments { get; set; }
  }
}
