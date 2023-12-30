using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server_Side.Core.Categories;
using Server_Side.Core.Categories.Models;
using Server_Side.Core.Profiles.Models;
using Server_Side.Core.Courses;

namespace Server_Side.MiddleWare.Backlog_sqlModels
{
    public class appDb: DbContext
    {
        public appDb(DbContextOptions<appDb>options):base(options) { }  

        public DbSet<Category>categories { get; set; }
        public DbSet<CategoryIMG>categoriesIMG { get; set; }
        public DbSet<Topic>topics { get; set; }



        public DbSet<Teacher> teachers { get; set; }
        public DbSet<TeacherIMG> teacherIMGs { get; set; }


        public DbSet<CourseDiscerption> courseDiscerptions { get; set;}
        public DbSet<CourseIMG> courseIMGs { get; set; }
        public DbSet<CourseVideo> courseVideos { get; set; }

    }
}
