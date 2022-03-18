using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMng.Data.Entites.Account;

namespace UserMng.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(c=>c.RoleTitle).IsRequired().HasMaxLength(50);

        }
    }
}
