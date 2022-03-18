using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMng.Data.Entites.Account;

namespace UserMng.Data.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {

        builder.HasKey(c => c.Id);
            


        builder.HasOne(c => c.Permission)
            .WithMany(c => c.RolePermissions)
            .HasForeignKey(c => c.PermissionId);


        builder.HasOne(c => c.Role)
            .WithMany(c => c.RolePermissions)
            .HasForeignKey(c => c.RoleId);



    }
}