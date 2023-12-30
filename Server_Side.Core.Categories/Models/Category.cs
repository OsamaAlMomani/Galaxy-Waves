namespace Server_Side.Core.Categories.Models
{
    public class Category
    {
        public Guid categoryId { get; set; }
        public string categoryName { get; set; }


        public Guid imgId { get; set; }
        public CategoryIMG img { get; set; }

        public Guid topicId { get; set; }
        public Topic topic { get; set; }
    }


}
