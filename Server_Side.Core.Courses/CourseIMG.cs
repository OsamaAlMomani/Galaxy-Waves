using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Server_Side.Core.Courses
{
    public class CourseIMG
    {
        public Guid imgID { get; set; }
        public List<string> imgLink { get; set; }
    }
}