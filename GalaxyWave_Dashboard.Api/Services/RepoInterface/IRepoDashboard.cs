using GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards;
using System.Collections;

namespace GalaxyWave_Dashboard.Api.Services.RepoInterface
{
    internal interface IRepoDashboard <T> 
        where T : class
    {

        public IEnumerable User_Dashboard_Profile(Guid Obj_Id);
        public IEnumerable User_Dashboard_AllList(T objX);
        public IEnumerable User_Dashboard_bring(T objX, int length);
        public IEnumerable User_Dashboard_Top();
        public IEnumerable User_Dashboard_Bottom();
        public void User_Dashboard_Creation(T objX);
        public void User_Dashboard_Delete(T objX);
        public void User_Dashboard_Update(T objX);
    }
}
