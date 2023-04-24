using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Core.Models.Guide
{
    public class GuideEquipment
    {
        [Key]
        public Guid? equipmentId { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Equipment Name")]
        public string? equipmentName { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Equipment Picture")]

        public string? equipmentImg { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Equipment Color")]
        public string? equipmentColor { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Equipment Type")]
        public string? equipmentType { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Equipment Brand")]
        public string? equipmentBrand { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Materials")]
        [DataType(DataType.MultilineText)]
        public string? Material { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        public decimal? Price { get; set; }
        
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name ="Show/Hide")]
        public bool? Hidden { get; set; }  

    }
}
