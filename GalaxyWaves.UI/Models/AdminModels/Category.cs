using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves.Models.AdminModels
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string? categoryName { get; set; }
    }
}
