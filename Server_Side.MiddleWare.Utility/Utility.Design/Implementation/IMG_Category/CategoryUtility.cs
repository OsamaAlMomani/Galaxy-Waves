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
            if (categoryName == null)                                                                  //  <== DOUBLE cHECK IF "category name is EXIST
            {                                                                                          //  <== DOUBLE cHECK IF "category name is EXIST
                 throw new Exception("Null Value for Category name");                                  //  <== DOUBLE cHECK IF "category name is EXIST
            }                                                                                          //  <== DOUBLE cHECK IF "category name is EXIST
                                                                                                       //  <== DOUBLE cHECK IF "category name is EXIST
            if (true)                                                                                  //  <== DOUBLE cHECK IF "category name is EXIST
            {                                                                                          //  <== DOUBLE cHECK IF "category name is EXIST
                bool FindSameValue = db.categories.Any(a => a.categoryName == categoryName);           //  <== DOUBLE cHECK IF "category name is EXIST
                if (FindSameValue)                                                                     //  <== DOUBLE cHECK IF "category name is EXIST
                    throw new Exception("\"" + categoryName + "\"" + "Is EXIST ");                     //  <== DOUBLE cHECK IF "category name is EXIST
            }                                                                                          //  <== DOUBLE cHECK IF "category name is EXIST


            Category newCategory = new Category
            {                                                                                        ///  Adding process 
                categoryId = Guid.NewGuid(),                                                         ///  Adding process 
                categoryName = categoryName                                                          ///  Adding process 
            };                                                                                       ///  Adding process 
            db.categories.Add(newCategory);                                                          ///  Adding process 
            db.SaveChanges();                                                                        ///  Adding process 
                                                                                                     ///  Adding process 
                                                                                                     ///  Adding process 
            if (true)
            {                                                                                        // <== Checking if New model Added successfully 
                bool FindSameValue = db.categories.Any(a => a.categoryName == categoryName);         // <== Checking if New model Added successfully 
                if (!FindSameValue)                                                                  // <== Checking if New model Added successfully 
                    throw new Exception("\"" + categoryName + "\"" + "Is NOT EXIST ");               // <== Checking if New model Added successfully 
            }                                                                                        // <== Checking if New model Added successfully 
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
