using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Models
{
    public class ContactNumber
    {
        public int UniversityId { get; set; }

        public int ContactNumberId { get; set; }

        public string PhoneNumber { get; set; }

        public virtual University University { get; set; } 
    }
}