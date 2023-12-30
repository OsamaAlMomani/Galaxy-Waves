using Server_Side.Core.Courses;
using Server_Side.MiddleWare.Backlog_sqlModels;
using Server_Side.MiddleWare.Utility.Utility.Design.Interface.ICourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.MiddleWare.Utility.Utility.Design.Implementation.IMP_Course
{
    public class CourseUtility : CourseInterface
    {
        private readonly appDb db;
        public CourseUtility(appDb _db)
        {
            db = _db;
        }
        public void addCourse(CourseDiscerption newCourse)
        {
            throw new NotImplementedException();
        }

        public void addImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void addVideo(Guid ID, string videoURL)
        {
            throw new NotImplementedException();
        }






        public void DeleteCourse(Guid ID)
        {
            throw new NotImplementedException();
        }

        public void DeleteImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void DeleteVideo(Guid ID, string videoURL)
        {
            throw new NotImplementedException();
        }






        public void UpdateCourse(Guid ID, CourseDiscerption updatedCourse)
        {
            throw new NotImplementedException();
        }

        public void UpdateImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void UpdateVideo(Guid ID, string videoURL)
        {
            throw new NotImplementedException();
        }
    }
}
