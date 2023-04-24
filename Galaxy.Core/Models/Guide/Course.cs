using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.Guide
{
    public class Course
    {
        public Guid courseId { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        public string? CourseName { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        public string? CourseDescription { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        public decimal? CoursePrice { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        public List<string> Level = new List<string>
        {
            "Beginner",
            "Intermediate",
            "Advance"
        };

        [Required]
        [ForeignKey("Email")]
        [Display(Name = "Teacher Email")]
        public string Email { get; set; }
        public Guide? Guide { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        public string? Duration { get; set; }

        //[ForeignKey("CourseVideo")]

        //public CourseVideo video{ get; set;}
    }
}
