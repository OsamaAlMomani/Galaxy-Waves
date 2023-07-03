using System.ComponentModel.DataAnnotations;

namespace GalaxyWaves.Models.SharedModels
{
    public class Profile : Links
    {
        [Key]
        public Guid Id { get; set; }
        public string? Role_Name { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DOB { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }

        public string? CardHolderName { get; set; }
        [Range(0, 999999999999, ErrorMessage = "maz digits are 12")]
        public string? CardNumber { get; set; }
        public string? CardDate { get; set; }
        [Range(0, 999, ErrorMessage = "number of digits must be 3")]
        public int? CVC { get; set; }

    }
}
