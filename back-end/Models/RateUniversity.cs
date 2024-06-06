using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Models
{
    public class RateUniversity
    {
        public int Id { get; set; }

        public int UniversityId { get; set; }

        public int UserId { get; set; }

        public int Overall { get; set; }

        public string Feedback { get; set; }

    }
}
