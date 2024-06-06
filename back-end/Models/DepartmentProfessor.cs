﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Models
{
    public class DepartmentProfessor
    {
        [Key, Column(Order =1)]  
        [ForeignKey("Department")]
        public int DepartmentId { get; set; } 

        [Key, Column(Order =2)]
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }

        public virtual Department Department {get; set;} 

        public virtual Professor Professor {get; set;}

        public ICollection<Department> Departments { get; set; }

        public ICollection<Professor> Professors { get; set; }
    }
}
