using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.AdminSite.Role
{
    public class CreateRole
    {
        [Required]
        public string? Name { get; set; }
    }
}
