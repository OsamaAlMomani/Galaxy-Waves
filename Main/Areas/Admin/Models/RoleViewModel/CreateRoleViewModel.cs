using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models.RoleViewModel
{
    public class CreateRoleViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? RoleName { get; set; }
    }
}
