using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Permissions;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.DataLayer.Context
{
   public class TopLearnContext:DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options):base(options)
        {

        }



        #region User

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        #endregion

        #region Wallet

        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        #region Course

            public DbSet<CourseGroup> CourseGroups { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(q=>!q.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<CourseGroup>()
                        .HasQueryFilter(g => !g.IsDelete);
            base.OnModelCreating(modelBuilder);
        }
    }
}
