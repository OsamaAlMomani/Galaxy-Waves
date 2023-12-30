using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.Core.Courses
{
    public class CourseVideo
    {
        public Guid videoId {  get; set; }
        public List<string> videoLink { get; set; }
    }
}
