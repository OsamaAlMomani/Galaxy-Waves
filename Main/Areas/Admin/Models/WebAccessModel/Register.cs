using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.WebAccessModel
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType (DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Password not matched")]
        public string? ConfirmPassword { get; set; }

    }
}
