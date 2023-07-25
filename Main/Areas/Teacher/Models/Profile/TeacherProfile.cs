using Main.Areas.SharedPersonalRefrance.Models;
using Main.Areas.Teacher.Models.Courses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Areas.Teacher.Models.Profile
{
    public class TeacherProfile : General_Info
    {
        [Key] public Guid TeacherProfile_Id { get; set; }
        [Required] public string? TeacherProfile_Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required] public string? TeacherProfile_Email { get; set; }
        [Required] public string? TeacherProfile_Pic { get; set; }
        public Guid courseCategory_Id { get; set; }
        [ForeignKey(nameof(courseCategory_Id))]
        public courseCategory? course { get; set; }
    }
}