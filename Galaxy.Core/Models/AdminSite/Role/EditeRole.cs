using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.AdminSite.Role
{
    public class EditeRole
    {
        public EditeRole()
        {
            User = new List<string>();
        }
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<string>? User;

    }
}
