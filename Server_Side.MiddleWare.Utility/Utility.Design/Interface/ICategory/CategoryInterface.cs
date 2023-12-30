using Server_Side.Core.Categories.Models;
using Server_Side.Core.Courses;
using Server_Side.MiddleWare.Backlog_sqlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.MiddleWare.Utility.Utility.Design.Interface.ICategory
{
    public interface CategoryInterface
    {
        void addImg(Guid ID, string imgURL);
        void addTopic(string topicName);
        void addCategory(string categoryName);


        void DeleteImg(Guid ID, string imgURL);
        void DeleteTopic(Guid ID,string topicName);
        void DeleteCategory(Guid ID,string categoryName);


        void UpdateImg(Guid ID, string imgURL);
        void UpdateTopic(Guid ID, string topicName);
        void UpdateCategory(Guid ID, Category updatedCategory);
    }
}
