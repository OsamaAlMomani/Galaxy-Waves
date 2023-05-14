using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Main.Models.DepartmentModels
{
    public class Core
    {
        [Key]
        public Guid CoreId { get; set; }
        [Required]
        public string? CoreName { get; set; }
        [Required]
        [ForeignKey("spec")]
        public Guid SpecId { get; set; }
        public Specialization? spec { get; set; }
    }
}
