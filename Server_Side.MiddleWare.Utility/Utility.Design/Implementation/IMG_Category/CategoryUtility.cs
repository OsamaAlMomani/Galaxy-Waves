using Server_Side.Core.Categories.Models;
using Server_Side.MiddleWare.Backlog_sqlModels;
using Server_Side.MiddleWare.Utility.Utility.Design.Interface.ICategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Side.MiddleWare.Utility.Utility.Design.Implementation.IMG_Category
{
    public class CategoryUtility : CategoryInterface
    {
        private readonly appDb db;
        public CategoryUtility(appDb _db)
        {
            db = _db;
        }
        public void addCategory(string categoryName)
        {
            if (categoryName == null)
            {
                 throw new Exception("Null Value for Category name");
            }

            if (true)
            {
                bool FindSameValue = db.categories.Any(a => a.categoryName == categoryName);
                if (FindSameValue)
                    throw new Exception("\"" + categoryName + "\"" + "Is EXCIT ");
            }

            Category newCategory = new Category
            {
                categoryId = Guid.NewGuid(),
                categoryName = categoryName
            };
            db.categories.Add(newCategory);
            db.SaveChanges();
            if (true)
            {
                bool FindSameValue = db.categories.Any(a => a.categoryName == categoryName);
                if (!FindSameValue)
                    throw new Exception("\"" + categoryName + "\"" + "Is NOT EXCIT ");
            }
        }

        public void addImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void addTopic(string topicName)
        {
            throw new NotImplementedException();
        }






        public void DeleteCategory(Guid ID, string categoryName)
        {
            throw new NotImplementedException();
        }

        public void DeleteImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void DeleteTopic(Guid ID, string topicName)
        {
            throw new NotImplementedException();
        }





        public void UpdateCategory(Guid ID, Category updatedCategory)
        {
            throw new NotImplementedException();
        }

        public void UpdateImg(Guid ID, string imgURL)
        {
            throw new NotImplementedException();
        }

        public void UpdateTopic(Guid ID, string topicName)
        {
            throw new NotImplementedException();
        }
    }
}
