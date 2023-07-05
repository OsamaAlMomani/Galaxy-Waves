using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves.Models.AdminModels
{
    public class SocialLinks
    {
        [Key]
        public string? LinkedIn { get; set; }

        [Required]
        public string? Facebook { get; set; }
        [Required]
        public string? Twitter { get; set; }
        [Required]
        public string? Gmail { get; set; }
        [Required]
        public string? Outlook { get; set; }
    }
}
