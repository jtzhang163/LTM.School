﻿using LTM.School.Application.enumsType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
  /// <summary>
  /// 登记表
  /// 学生和课程之间的对应关系
  /// </summary>
  public class Enrollment
  {
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    [DisplayFormat(NullDisplayText = "暂无成绩")]
    public CourseGrade? Grade { get; set; }

    public Student Student { get; set; }

    public Course Course { get; set; }
  }
}
