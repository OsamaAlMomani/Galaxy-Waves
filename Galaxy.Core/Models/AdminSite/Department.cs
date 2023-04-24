using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.AdminSite
{
    public class Department
    {
        [Key]
        public Guid DepartmentId {  get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name ="Equipment Name")]
        public string? DepartmentName { get; set; }
        
    }
}
