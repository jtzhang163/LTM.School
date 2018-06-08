using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.ViewModels
{
  public class EnrollmentDateGroup
  {
    /// <summary>
    /// 学生个数
    /// </summary>
    [DisplayName("学生个数")]
    public int Count { get; set; }

    /// <summary>
    /// 注册日期
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayName("注册日期")]
    public DateTime? EnrollmentDate { get; set; }
  }
}
