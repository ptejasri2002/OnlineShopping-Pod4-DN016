using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OnlineShoppingTeam4.Models;
using Microsoft.AspNet.Identity.EntityFramework;
namespace OnlineShoppingTeam4.Context
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationDbContext()
            : base("name=DefaultConnection") // name in web.config of Connection string.
        {
            // Database.SetInitializer(new NorthwindDatabaseInitializer());
        }

        /// <summary>
        /// Returns a new instance of this class.
        /// </summary>
        /// <returns>new ApplicationDbContext</returns>
        public static Models.ApplicationDbContext Create()
        {
            return new Models.ApplicationDbContext();
        }

        /// <summary>
        /// When model start to be build.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");


        }
    }
}