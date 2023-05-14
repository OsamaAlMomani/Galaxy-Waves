using Main.Models.AdminModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Models.DepartmentModels
{
    public class Specialization
    {
        [Key]
        public Guid SpecId { get; set; }
        [Required]
        public string? SpecName { get; set; }
        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
