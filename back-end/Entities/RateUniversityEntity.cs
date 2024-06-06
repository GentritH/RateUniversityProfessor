using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RateForProfessor.Entities.Identity;

namespace RateForProfessor.Entities
{
    public class RateUniversityEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("University")]
        public int UniversityId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int Overall { get; set; }

        public string Feedback { get; set; }

        public virtual UniversityEntity University { get; set; }

        public virtual User User { get; set; }
    }
}
