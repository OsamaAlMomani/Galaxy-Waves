using Main.Areas.Teacher.Models.Profile;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Main.Models.DepartmentModels;

namespace Main.Areas.Teacher.Models.UI_Model
{
    public class courseCategory_ViewModel
    {
        [Key] public Guid courseCategory_Id { get; set; }
        [Required] public string? courseCategory_Name { get; set; }
        [Required] public string? courseCategory_Description { get; set; }
        [Required] public IFormFile? courseCategory_Img { get; set; }
        [Required] public decimal? courseCategory_Price { get; set; }


        [Required]
        public Guid CoreId { get; set; }
        [ForeignKey(nameof(CoreId))]
        public Core? core { get; set; }
    }
}
