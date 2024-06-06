using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Models
{
    public class Course
    {
        public int ID { get; set; }

        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int CreditHours { get; set; }

        public string Description { get; set; }

    }
}
