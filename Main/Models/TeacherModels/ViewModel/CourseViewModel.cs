using Main.Models.AdminModels;
using Main.Models.DepartmentModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Models.TeacherModels.ViewModel
{
    public class CourseViewModel
    {
        [Key]
        public Guid CourseId { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string? CourseName { get; set; }
        [Required]
        [Display(Name = "About the Course")]
        public string? CourseDescription { get; set; }

        [Required]
        [Display(Name = "Course Img")]
        public IFormFile? CourseImg { get; set; }

        [Required]
        public IFormFile? CourseVideo { get; set; }
        [Required]
        [ForeignKey("Core")]
        public Guid CoreId { get; set; }
        public Core? Core { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
