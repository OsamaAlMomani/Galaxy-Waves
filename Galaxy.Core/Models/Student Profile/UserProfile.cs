using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.Student_Profile
{
    public class UserProfile
    {
        [Key]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Full Name ")]
        public string? Full_Name { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? cEmail { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [Display(Name = "Profile Picture")]
        public string? Img { get; set; }

        [Display(Name = "Date of Birthday")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Gender")]
        public string? Sex { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "City")]
        public string? City { get; set; }
        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Display(Name = "Date Of Hiring")]
        public DateTime JDate { get; set; }


        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Card Name")]
        public string? CardName { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "Credit Card")]
        public Int64 CreditCard { get; set; }

        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "CVV")]
        
        public int CVV { get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name = "EXP DATE")]
        public DateTime CardEXP { get; set; }


        

    }
}
