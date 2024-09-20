using authentication_security.Core.Domain.Entities;
namespace authentication_security.Core.Application.Interfaces;
public interface ITokenRepository
{
    Task AddUserToken(UserToken userToken);
    Task InvalidateUserToken(string userId);
    Task<UserToken?> GetByUserId(string userId);
}

