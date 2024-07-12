using System.ComponentModel.DataAnnotations;

namespace MMA.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Course Name")]
        public string CourseName { get; set; }
        public string Topic { get; set; }
        public string Image { get; set; }
        public double Price { get; set; } 
        public string Description { get; set; }
        [Display(Name ="Teacher Name")]
        public string TeacherName { get; set; }

    }
}
