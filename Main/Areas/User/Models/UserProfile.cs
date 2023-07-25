using System.ComponentModel.DataAnnotations;

namespace Main.Areas.User.Models
{
    public class UserProfile
    {
        [Key] public Guid UserId { get; set; }
        [Required] public string? User_fullName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required] public string? User_email { get; set; }
        
        /*
         1- Courses
         2- Certificate
         3- Wallet
         */
    }
}
