using System.ComponentModel.DataAnnotations;

namespace Main.Areas.WebAccess.Models.WebAccessModel
{
    public class SignIn
    {
        [Required]
        [DataType(DataType.EmailAddress)]
       public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
