using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMng.Data.Entites.Account;

namespace UserMng.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {

            builder.HasKey(c => c.Id);
           

            builder.HasOne(c=>c.Role).WithMany(c=>c.userRoles).HasForeignKey(c=>c.RoleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c=>c.user).WithMany(c=>c.userRoles).HasForeignKey(c=>c.UserId).OnDelete(DeleteBehavior.Restrict);
            
        }
    }


    


}
