using GalaxyWave_Dashboard.Api.Models.UsersDashboards.Elements;
using GalaxyWave_Dashboard.Api.Models.UsersDashboards;

namespace GalaxyWave_Dashboard.Api.Services.RepoInterface
{
    public interface IRepoDashboard <T,U> 
        where T : class
        where U : class
    {
        public void User_Dashboard_Creation(T objX);
        public void User_Dashboard_Delete(T objX);
        public void User_Dashboard_Update(T objX, U ObjY);
        public IEnumerable<U> User_Dashboard_Profile(T objX);
        public IEnumerable<U> User_Dashboard_AllList(T objX);
        public IEnumerable<U> User_Dashboard_bring(T objX, int length, U ObjY);
        public IEnumerable<U> User_Dashboard_Top();
        public IEnumerable<U> User_Dashboard_Bottom();
    }
}
