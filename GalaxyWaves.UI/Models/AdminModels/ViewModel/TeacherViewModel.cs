using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves.Models.AdminModels.ViewModel
{
    public class TeacherViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Full Name ")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Profile Picture")]
        public IFormFile? Img { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Date of Birthday")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Gender")]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [DataType(DataType.MultilineText)]
        public string? About { get; set; }
    }
}
