using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves_UI.Models.AdminModels
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string? categoryName { get; set; }
    }
}
