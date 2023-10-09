﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GalaxyWave.SQL_Model.Models.UsersDashboards.Elements;
using GalaxyWave.SQL_Model.Models.UsersDashboards;

namespace GalaxyWave_repoDesign.Core.RepoInterface
{
    internal interface IDashboard
    {
        public void User_Dashboard_Creation(User user );
        public void User_Dashboard_Delete(User user );
        public void User_Dashboard_Update(User user );
        public void User_Dashboard_View(User user );
    }
}
