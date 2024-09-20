
using authentication_security.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication_security.Infraestructure.EntityConfigurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(typeof(UserToken).Name);
        builder.HasKey(x => x.Id);

        #region Config Properties

        builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(36);

        builder.HasIndex(x => x.UserId);

        builder.Property(x => x.Token)
               .IsRequired()
               .HasMaxLength(1000)
               .HasColumnName("Token")
               .HasColumnType("nvarchar");

        builder.Property(x => x.IsValid)
               .HasColumnType("BIT")
               .HasDefaultValue(true);

        builder.Property(x => x.StartSession)
               .IsRequired();

        builder.Property(x => x.EndSession)
               .IsRequired();

        builder.Property(x => x.TimeInSession)
               .IsRequired()
               .HasMaxLength(50);

        #endregion
    }
}
