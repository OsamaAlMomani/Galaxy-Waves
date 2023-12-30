using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.Core.Courses.ViewModel
{
    public  class CourseIMG_ViewModel
    {
        public Guid imgID {  get; set; }
        public IFormFile imgLink { get; set; }
    }
}
