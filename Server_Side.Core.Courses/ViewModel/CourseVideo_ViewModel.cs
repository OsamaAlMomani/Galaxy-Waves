using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.Core.Courses.ViewModel
{
    public  class CourseVideo_ViewModel
    {
        public Guid videoId { get; set; }
        public IFormFile videoLink { get; set; }
    }
}
