using authentication_security.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication_security.Infraestructure.EntityConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(typeof(User).Name);
        builder.HasKey(x => x.Id);

        #region Config Properties

        builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(16);
        builder.HasIndex(x => x.UserName);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(30);

        builder.HasIndex(x => x.Email);

        builder.Property(x => x.EmailConfirmed)
               .HasColumnType("BIT");

        builder.Property(x => x.Password)
               .IsRequired()
               .HasMaxLength(255);

        #endregion
    }
}
