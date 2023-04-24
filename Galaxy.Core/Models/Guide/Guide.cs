using Galaxy.Core.Models.C_Level.Departments;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.Guide
{
    public class Guide
    {
        [Key]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "First Name ")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Last Name ")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Profile Picture")]

        public string? Img { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Date of Birthday")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Gender")]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "City")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Date Of Hiring")]
        public DateTime JDate { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Working(Active) / On Vacation(Idle) / Retired(UnActive)")]
        public List<string> Status = new List<string>()
        {
            "Active",
            "Idel",
            "UnActive"
        };

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Bank IBan")]
        public long IBan { get; set; }

        //**---------------**\\

        // Relationships with other parts of the Company

        // Department Table 
        [ForeignKey("DepartmentName")]
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "C Level Department")]
        public string? DepartmentName { get; set; }
        public Department? department { get; set; }
        // Position Table
        [ForeignKey("positionName")]
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Position Name")]
        public string? positionName { get; set; }
        public Position? position { get; set; }
        // Equipment Table
        [ForeignKey("equipmentId")]
        public Guid equipmentId { get; set; }
        public GuideEquipment? equipment { get; set; }
        //**---------------**\\ 
    }
}
