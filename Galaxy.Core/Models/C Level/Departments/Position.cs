using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.C_Level.Departments
{
    public class Position
    {
        [Key]
        public Guid positionId {  get; set; }
        [Required(ErrorMessage = "Empty Field")]
        [Display(Name ="New Position")]
        public string? positionName { get; set;}
    }
}
