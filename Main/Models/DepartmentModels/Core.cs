using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Main.Models.DepartmentModels
{
    public class Core
    {
        [Key]
        public int CoreId { get; set; }
        [Required]
        public string? CoreName { get; set; }
        [Required]
        [ForeignKey("spec")]
        public int SpecId { get; set; }
        public Specialization? spec { get; set; }
    }
}
