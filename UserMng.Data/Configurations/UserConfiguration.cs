using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMng.Data.Entites.Account;

namespace UserMng.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(c=>c.UserName).IsRequired().HasMaxLength(50);
            builder.Property(c=>c.Email).IsRequired().HasMaxLength(50);
            builder.Property(c=>c.ActiveCode).HasMaxLength(250);
            builder.Property(c=>c.RegisterDate).HasMaxLength(50);
            
        }
    }


    


}
