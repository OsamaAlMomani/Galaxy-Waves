using Server_Side.Core.Profiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.MiddleWare.Utility.Utility.Design.Interface.IProfile
{
    public interface ProfileInterface
    {
        void addImg(Guid ID, string imgURL);
        void addTeacher(Guid ID , Teacher newTeacher);



        void deleteImg(Guid ID, string imgURL);
        void deleteTeacher(Guid ID, Teacher newTeacher);

        void UpdateImg(Guid ID, string imgURL);
        void UpdateTeacher(Guid ID, Teacher newTeacher);
    }
}
