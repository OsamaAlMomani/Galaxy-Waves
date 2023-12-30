using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.Core.Profiles.Models.ViewModel
{
    public class TeacherIMG_viewModel
    {
        public Guid imgId { get; set; }
        public IFormFile imgName { get; set; }
    }
}
