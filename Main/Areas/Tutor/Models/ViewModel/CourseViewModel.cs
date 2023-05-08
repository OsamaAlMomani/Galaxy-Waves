using Main.Areas.Admin.Models;
using Main.Areas.Departments.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Tutor.Models.ViewModel
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
        public IFormFile? CourseVideo { get; set; }
        [Required]
        [ForeignKey("Teacher")]
        public Guid TeacherID { get; set; }
        public Teacher? teacher { get; set; }
        [Required]
        [ForeignKey("Category")]
        public Guid CategoryID { get; set; }
        public Category? Category { get; set; }
        [Required]
        [ForeignKey("Specialization")]
        public int SpecId { get; set; }
        public Specialization? Specialization { get; set; }
        [Required]
        [ForeignKey("Core")]
        public int CoreId { get; set; }
        public Core? Core { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
