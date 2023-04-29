﻿using Galaxy.Core.Models.AdminSite;
using Galaxy.Core.Models.Guide;
using Galaxy.Core.ViewModelComponent;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Data.DataAccess
{
    public class AppDbContext :IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options){ }

        public DbSet<TeacherProfile> Teacher_Profile { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Equipment> equipment { get; set; }
        public DbSet<LoginViewModel> LoginViewModel { get; set; }
        public DbSet<RegisterViewModel> RegisterViewModel { get; set; }
        public DbSet<Course> Course { get; set; }

    }
}
