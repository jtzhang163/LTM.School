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
    [Display(Name = "课程编号")]
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 2)]
    [Display(Name = "名称")]
    public string Title { get; set; }
    /// <summary>
    /// 学分
    /// </summary>
    [Range(0,5)]
    [Display(Name = "学分")]
    public int Credits { get; set; }

    /// <summary>
    /// 部门Id
    /// </summary>
    public int DepartmentId { get; set; }

    [Display(Name = "部门")]
    public Department Department { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }

    public ICollection<CourseAssignment> CourseAssignments { get; set; }
  }
}
