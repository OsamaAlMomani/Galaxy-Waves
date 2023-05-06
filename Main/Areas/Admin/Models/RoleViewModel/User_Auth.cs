using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.RoleViewModel
{
    public class User_Auth
    {
        public Guid Id { get; set; }
        [Required]
        public string? UserName { get; set; }       
    }
}
