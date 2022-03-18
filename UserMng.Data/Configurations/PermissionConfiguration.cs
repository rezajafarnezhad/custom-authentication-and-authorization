using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMng.Data.Entites.Account;

namespace UserMng.Data.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {

        builder.HasKey(c => c.Id);
        builder.Property(c=>c.PermissionTitle).IsRequired().HasMaxLength(50);


        builder.HasOne(c => c.permission)
            .WithMany(c => c.Permissions)
            .HasForeignKey(c => c.ParenPermissionId);


    }
}