using System.ComponentModel.DataAnnotations;

namespace GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserDescription { get; set; }
        public string UserEmail { get; set; }

        public DateTime DOB { get; set; }
        public string password { get; set; }

        public string Gender { get; set; }


    }
}
