using GalaxyWave_Dashboard.Api.AccessData;
using GalaxyWave_Dashboard.Api.Services.RepoInterface;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Collections;

namespace GalaxyWave_Dashboard.Api.Services.RepoClass
{
    public class RepoDashboard<T> : IRepoDashboard<T> 
        where T :class
    {
        private AppDb db;
        public RepoDashboard(AppDb _db)
        {
            db= _db;    
        }

        public IEnumerable User_Dashboard_AllList(T objX)
        {
            throw new NotImplementedException();
        }

        public IEnumerable User_Dashboard_Bottom()
        {
            throw new NotImplementedException();
        }

        public IEnumerable User_Dashboard_bring(T objX, int length)
        {
            throw new NotImplementedException();
        }

        public void User_Dashboard_Creation(T objX)
        {
            throw new NotImplementedException();
        }

        public void User_Dashboard_Delete(T objX)
        {
            throw new NotImplementedException();
        }

        public IEnumerable User_Dashboard_Profile(Guid Obj_Id)
        {
            if (Obj_Id != null )
            {
                 var ReturnedValue = db.dashboards.Include(a=>a.User).FirstOrDefault(a=>a.User.UserId == Obj_Id).ToJson();
                return ReturnedValue;
            }
            return null;

        }

        public IEnumerable User_Dashboard_Top()
        {
            throw new NotImplementedException();
        }

        public void User_Dashboard_Update(T objX)
        {
            throw new NotImplementedException();
        }
    }
}
