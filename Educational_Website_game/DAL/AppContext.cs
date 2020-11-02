using LibraryDeweyApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.DAL
{
    //don't use this context as want a single db
    public class AppContext : IdentityDbContext<ApplicationUser>
    {
        public AppContext()
        : base("AppContext", throwIfV1Schema: false)
        {
        }

        public DbSet<Leaderboards> Leaderboards { get; set; }
        public DbSet<LeaderResults> LeaderResults { get; set; }
        public DbSet<Results> Results { get; set; }
    }
}