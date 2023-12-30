using Server_Side.Core.Profiles.Models;
using Server_Side.MiddleWare.Backlog_sqlModels;
using Server_Side.MiddleWare.Utility.Utility.Design.Interface.IProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.MiddleWare.Utility.Utility.Design.Implementation.IMP_Teacher
{
    public class TeacherUtility : ProfileInterface
    {
        private readonly appDb db;
        public TeacherUtility(appDb _db)
        {
            db = _db;
        }
        public void addImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void addTeacher(Guid ID, Teacher newTeacher)
        {
            throw new NotImplementedException();
        }




        public void deleteImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void deleteTeacher(Guid ID, Teacher newTeacher)
        {
            throw new NotImplementedException();
        }




        public void UpdateImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void UpdateTeacher(Guid ID, Teacher newTeacher)
        {
            throw new NotImplementedException();
        }
    }
}
