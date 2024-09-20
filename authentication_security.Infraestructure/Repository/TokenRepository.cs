using authentication_security.Core.Application.Interfaces;
using authentication_security.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace authentication_security.Infraestructure.Repository;
public class TokenRepository(AppContext context) : ITokenRepository
{
    private readonly AppContext _context = context;
    protected DbSet<UserToken> Entity => _context.Set<UserToken>();

    public async Task AddUserToken(UserToken userToken)
    {
        await Entity.AddAsync(userToken);
        await _context.SaveChangesAsync();
    }

    public async Task InvalidateUserToken(string userId)
    {
        UserToken? userTokenInDb = await GetByUserId(userId);

        if (userTokenInDb != null)
        {
            userTokenInDb.IsValid = false;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<UserToken?> GetByUserId(string userId) => await Entity.FirstOrDefaultAsync(u => u.UserId == userId);
}
