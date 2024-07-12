using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MMA.Models
{
    public class InformationWeb
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
       

    }
}
