using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  /// <summary>
  /// 老师
  /// </summary>
  public class Instructor : Person
  {

    [Display(Name = "入职日期")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime HireDate { get; set; }

    public ICollection<CourseAssignment> CourseAssignments { get; set; }

    public OfficeAssignment OfficeAssignment { get; set; }
  }
}
