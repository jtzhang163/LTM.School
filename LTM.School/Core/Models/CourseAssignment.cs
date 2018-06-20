using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  /// <summary>
  /// 课程分配
  /// </summary>
  public class CourseAssignment
  {
    /// <summary>
    /// 教师Id
    /// </summary>
    public int InstructorId { get; set; }

    public Instructor Instructor { get; set; }

    /// <summary>
    /// 课程Id
    /// </summary>
    public int CourseId { get; set; }

    public Course Course { get; set; }
  }
}
