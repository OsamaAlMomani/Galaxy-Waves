using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves_UI.Models.AdminModels.RoleViewModel
{
    public class CreateRoleViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? RoleName { get; set; }
    }
}
