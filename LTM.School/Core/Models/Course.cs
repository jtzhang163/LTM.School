using LTM.School.Application.enumsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Core.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }
        /// <summary>
        /// 学分
        /// </summary>
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
