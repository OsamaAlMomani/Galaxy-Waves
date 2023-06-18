using GalaxyWaves.Models.AdminModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyWaves.Models.AdminModels.ViewModel
{
    public class MusicEquipmentViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [ForeignKey("Category")]
        public Guid? categoryName { get; set; }
        public Category? Category { get; set; }
        [Required]
        public IFormFile? Img { get; set; }
        [Required]
        [Display(Name = "About It")]
        public string? About_It { get; set; }
        [Required]
        public decimal? Price { get; set; }
    }
}
