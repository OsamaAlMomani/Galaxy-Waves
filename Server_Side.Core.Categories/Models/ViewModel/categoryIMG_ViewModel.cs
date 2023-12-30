using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.Core.Categories.Models.ViewModel
{
    public class categoryIMG_ViewModel
    {
        public Guid imgId { get; set; }
        public IFormFile imgName { get; set; }
    }
}
