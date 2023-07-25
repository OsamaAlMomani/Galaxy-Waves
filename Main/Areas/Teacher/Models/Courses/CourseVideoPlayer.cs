using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Areas.Teacher.Models.Courses
{
    public class CourseVideoPlayer
    {
        [Key] public Guid CourseVideoPlayer_Id { get; set; }
        [Required] public string? CourseVideoPlayer_Title { get; set; }
        [Required] public string? CourseVideoPlayer_Description { get; set; }
        [Required] public string? CourseVideoPlayer_Video { get; set; }
        //Owner
        public Guid courseCategory_Id { get; set; }
        [ForeignKey(nameof(courseCategory_Id))]

        public courseCategory? CourseCatefory { get; set; }

    }
}
