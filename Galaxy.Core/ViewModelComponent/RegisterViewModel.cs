using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Galaxy.Core.ViewModelComponent
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required]
        [Key]
        public string? EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Not match!")]
        public string? PasswordConfirmation { get; set;}
        

    }
}
