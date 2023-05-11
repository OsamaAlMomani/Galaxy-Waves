using Main.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Main.Areas.Departments.Models
{
    public class Core
    {
        [Key]
        public int CoreId { get; set; }
        [Required]
        public string? CoreName { get; set; }
        [Required]
        [ForeignKey("SpecId")]
        public int SpecId { get; set; }
        public Specialization? spec { get; set; }
    }
}
