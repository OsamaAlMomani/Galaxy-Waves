using Server_Side.Core.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.MiddleWare.Utility.Utility.Design.Interface.ICourse
{
    public interface CourseInterface
    {
        void addImg(Guid ID , string imgURL );
        void addVideo(Guid ID , string videoURL );
        void addCourse(CourseDiscerption newCourse );


        void DeleteImg(Guid ID, string imgURL);
        void DeleteVideo(Guid ID, string videoURL);
        void DeleteCourse(Guid ID);


        void UpdateImg(Guid ID, string imgURL);
        void UpdateVideo(Guid ID, string videoURL);
        void UpdateCourse(Guid ID, CourseDiscerption updatedCourse);

    }
}
