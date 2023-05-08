using Main.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Areas.Departments.Models
{
    public class Specialization
    {
        [Key]
        public int SpecId { get; set; }
        [Required]
        public string? SpecName { get; set; }
        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
