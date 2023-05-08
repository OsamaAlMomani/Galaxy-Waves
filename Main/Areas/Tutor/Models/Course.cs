using Main.Areas.Admin.Models;
using Main.Areas.Departments.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace Main.Areas.Tutor.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string? CourseName { get; set; }
        [Required]
        [Display(Name ="About the Course")]
        public string? CourseDescription { get; set; }
        [Required]
        public string ? CourseVideo { get; set;}
        [Required]
        [ForeignKey("Teacher")]
        public Guid TeacherID { get; set; }
        public Teacher? teacher { get; set; }
        [Required]
        [ForeignKey("Core")]
        public int CoreId { get; set; }
        public Core? Core { get; set; }
        [Required]
        public decimal Price { get; set; }
        
    }
}
