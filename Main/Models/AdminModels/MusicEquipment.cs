using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Models.AdminModels
{
    public class MusicEquipment
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
        public string? Img { get; set; }
        [Required]
        [Display(Name = "About It")]
        public string? About_It { get; set; }
        [Required]
        public decimal? Price { get; set; }
    }
}
