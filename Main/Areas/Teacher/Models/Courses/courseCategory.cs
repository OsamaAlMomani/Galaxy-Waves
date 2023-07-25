using Main.Areas.Teacher.Models.Profile;
using Main.Models.DepartmentModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Areas.Teacher.Models.Courses
{
    public class courseCategory
    {
        [Key] public Guid courseCategory_Id { get; set; }
        [Required] public string? courseCategory_Name { get; set; }
        [Required] public string? courseCategory_Description { get; set; }
        [Required] public string? courseCategory_Img { get; set; }
        [Required] public decimal? courseCategory_Price { get; set; }


        [Required]
        public Guid CoreId { get; set; }
        [ForeignKey(nameof(CoreId))]
        public Core? core { get; set; }


    }
}
