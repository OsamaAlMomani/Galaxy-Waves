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
        [ForeignKey("Specialization")]
        public int SpecId { get; set; }
        public Specialization? specialization { get; set; }
    }
}
