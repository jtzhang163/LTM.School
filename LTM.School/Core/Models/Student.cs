using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  /// <summary>
  /// 学生
  /// </summary>
  public class Student : Person
  {

    [DisplayName("注册时间")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime EnrollmnetDate { get; set; }

    [DisplayName("登记信息")]
    public ICollection<Enrollment> Enrollments { get; set; }

    [StringLength(200)]
    public string Secret { get; set; }
  }
}
