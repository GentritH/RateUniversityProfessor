using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RateForProfessor.Models
{
    public class Address
    {
        public int UniversityId { get; set; }

        public int AddressId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int ZIPCode { get; set; }

        public virtual University University { get; set; }
    }
}