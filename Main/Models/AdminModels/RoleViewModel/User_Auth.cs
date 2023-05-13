using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Models.AdminModels.RoleViewModel
{
    public class User_Auth
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
