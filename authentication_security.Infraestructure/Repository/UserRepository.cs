
using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace authentication_security.Infraestructure.Repository;
public class UserRepository(AppContext context) : IUserRepository
{
    private readonly AppContext _context = context;
    protected DbSet<User> Entity => _context.Set<User>();

    public async Task InsertUserAsync(User user)
    {
        await Entity.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user, string id)
    {
        var entry = await Entity.FindAsync(id);
        _context.Entry(entry).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(string id) => await Entity.FindAsync(id);
    public async Task<User?> GetUserByEmail(string email) => await Entity.SingleOrDefaultAsync(u => u.Email == email);
    public async Task<User?> GetUserByUserName(string userName) => await Entity.SingleOrDefaultAsync(u => u.UserName == userName);

}
