using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves_Media.Areas.admin.Models.RoleManager
{
    public class CreateRole
    {
        [Key] public int RoleId { get; set; }
        [Required] public string? RoleName { get; set; }
    }
}
