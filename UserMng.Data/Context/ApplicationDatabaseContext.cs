using Microsoft.EntityFrameworkCore;
using UserMng.Data.Configurations;
using UserMng.Data.Entites.Account;

namespace UserMng.Data.Context
{
    public class ApplicationDatabaseContext : DbContext
    {

        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> op):base(op)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions{ get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var asb = typeof(RoleConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(asb);

            base.OnModelCreating(modelBuilder);
        }

    }
}
