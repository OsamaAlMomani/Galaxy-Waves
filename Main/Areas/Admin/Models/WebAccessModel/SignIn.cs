using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.WebAccessModel
{
    public class SignIn
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        string? Email { get; set; }
        [Required]
        [DataType (DataType.Password)]
        string? Password { get; set; }
    }
}
