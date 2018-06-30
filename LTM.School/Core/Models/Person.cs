using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  public class Person
  {
    public int Id { get; set; }

    [Required]
    [DisplayName("姓名")]
    [StringLength(8, ErrorMessage = "输入名字过长")]
    public string RealName { get; set; }
  }
}