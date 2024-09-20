using authentication_security.Core.Domain.Entities;
using authentication_security.Infraestructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace authentication_security.Infraestructure;
public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

