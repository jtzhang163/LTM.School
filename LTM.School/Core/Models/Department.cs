﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  /// <summary>
  /// 部门
  /// </summary>
  public class Department
  {

    public int Id { get; set; }


    [StringLength(50, MinimumLength = 2)]
    [Display(Name = "名称")]
    public string Name { get; set; }

    /// <summary>
    /// 预算
    /// </summary>
    [Column(TypeName = "money")]
    [Display(Name = "预算")]
    [DataType(DataType.Currency)]
    public decimal Budget { get; set; }

    /// <summary>
    /// 开课时间
    /// </summary>
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "开课时间")]
    public DateTime StartDate { get; set; }

    public int? InstructorId { get; set; }

    /// <summary>
    /// 办公室主任
    /// </summary>
    [Display(Name = "办公室主任")]
    public Instructor Administrator { get; set; }

    /// <summary>
    /// 课程列表
    /// </summary>
    public ICollection<Course> Courses { get; set; }

    /// <summary>
    /// 高并发下。。。
    /// </summary>
    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}
