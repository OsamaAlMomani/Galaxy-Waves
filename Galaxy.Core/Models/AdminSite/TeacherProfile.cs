using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Core.Models.AdminSite
{
    public class TeacherProfile
    {
        [Key]
        public Guid TeacherID { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "First Name ")]
        public string? TeacherFirstName { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Last Name ")]
        public string? TeacherLastName { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? TeacherEmail { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Phone Number")]
        public string? TeacherPhone { get; set; }

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
        [Display(Name = "Bank IBan")]
        public Int64 IBan { get; set; }

                //**---------------**\\

        // Relationships with other parts of the Company
            
                    // Department Table 
        [ForeignKey("DepartmentName")]
        [Display(Name = "C Level Department")]
        public string? DepartmentName { get; set; }
        public Department? department { get; set; }
                    // Equipment Table
        [ForeignKey("equipmentId")]

        public Guid equipmentId { get; set; }
        public Equipment? equipment { get; set; }
               //**---------------**\\ 
    }
}
