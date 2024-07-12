using System.ComponentModel.DataAnnotations;

namespace MMA.Models
{
    public class BaseEntity
    {
    
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set;}
        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set;}
    }
}
