using Galaxy.Core.ViewModelComponent;
using Microsoft.Build.Framework;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.Models.AdminSite.Role
{
    public class Authorized
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool? IsSelected { get; set; }
    }
}
