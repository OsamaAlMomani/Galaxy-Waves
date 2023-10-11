using GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Models.UsersDashboards.Elements;
using System.ComponentModel.DataAnnotations;

namespace GalaxyWave_Dashboard.Api.Areas.Dashboard_GetUser.Models.UsersDashboards
{
    public class Dashboard
    {
        [Key]
        public Guid DashboardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        /* Foreinkey */
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}




/*
 
        USer =Has One=> Dashboard
        But {
                Dashboard Created with USer Creation 
                    Dashboard = Has => User 
                        'Any other Tools Call it Something'
                "Something may have many of object and shapes all belongs to Specific Dashboard" 
                                                                                

                                                                           Something
                                                                            /
                                                                        Dashboard
                                                                       /         
                                                                     User           
            }   
 */