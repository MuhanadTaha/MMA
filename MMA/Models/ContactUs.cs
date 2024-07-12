using System.ComponentModel.DataAnnotations;

namespace MMA.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
