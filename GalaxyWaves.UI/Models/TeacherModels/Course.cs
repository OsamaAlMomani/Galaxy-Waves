using GalaxyWaves.Models.DepartmentModels;
using GalaxyWaves.Models.AdminModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyWaves.Models.TeacherModels
{
    public class Course
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
        public string? CourseImg { get; set; }

        [Required]
        public string? CourseVideo { get; set; }
        [Required]
        [ForeignKey("Core")]
        public Guid CoreId { get; set; }
        public Core? Core { get; set; }
        [Required]
        public int Price { get; set; }

    }
}
