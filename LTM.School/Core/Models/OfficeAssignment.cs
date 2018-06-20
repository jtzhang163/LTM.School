using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  /// <summary>
  /// 办公室分配
  /// </summary>
  public class OfficeAssignment
  {
    /// <summary>
    /// 讲师Id
    /// </summary>
    [Key]
    public int InstructorId { get; set; }

    /// <summary>
    /// 教师的导航属性
    /// </summary>
    public Instructor Instructor { get; set; }
    /// <summary>
    /// 办公室位置
    /// </summary>
    [StringLength(50)]
    [Display(Name = "办公室位置")]
    public string Location { get; set; }
  }
}
