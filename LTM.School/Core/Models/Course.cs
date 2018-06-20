using LTM.School.Application.enumsType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  /// <summary>
  /// 课程
  /// </summary>
  public class Course
  {
    [Display(Name = "Number")]
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 2)]
    public string Title { get; set; }
    /// <summary>
    /// 学分
    /// </summary>
    [Range(0,5)]
    public int Credits { get; set; }

    /// <summary>
    /// 部门Id
    /// </summary>
    public int DepartmentId { get; set; }

    public Department Department { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }

    public ICollection<CourseAssignment> CourseAssignments { get; set; }
  }
}
