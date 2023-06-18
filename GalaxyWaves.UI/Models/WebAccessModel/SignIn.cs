using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves.Models.WebAccessModel
{
    public class SignIn
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
