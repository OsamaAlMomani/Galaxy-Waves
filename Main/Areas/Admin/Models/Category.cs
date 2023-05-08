using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Admin.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string? categoryName { get; set; }
    }
}
