using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.Core.Profiles.Models
{
    public class Teacher
    {
        public Guid teacherId {get;set;}
        public string teacherName { get;set;}
        public List<string> EMail {  get;set;}
        public string Description { get;set;}
        public Guid imgId { get;set;}
        public TeacherIMG img {  get;set;}

    }
}
