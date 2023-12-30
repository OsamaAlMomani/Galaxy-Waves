using Server_Side.Core.Categories.Models;
using Server_Side.Core.Profiles.Models;

namespace Server_Side.Core.Courses
{
    public class CourseDiscerption
    {
        public Guid courseId { get; set; }
        public string courseName { get; set; }
        public string courseDescription { get; set; }
        public Guid videoId { get; set; }
        public CourseVideo courseVideo { get; set; }

        public Guid imgId { get; set; }
        public CourseIMG img { get; set; }

        public DateTime Creation { get; set; }

        public float Duration { get; set; }

        public decimal Price { get; set; }



        public Guid TeacherId { get; set; }
        public Teacher teacher { get; set; }


        public Guid categoryId { get; set; }
        public Category category { get; set; }
    }
}
