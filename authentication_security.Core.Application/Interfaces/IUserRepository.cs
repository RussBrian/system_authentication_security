using authentication_security.Core.Domain.Entities;

namespace authentication_security.Core.Application.Interfaces;
public interface IUserRepository
{
    Task InsertUserAsync(User user);
    Task<User?> GetUserById(string id);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByUserName(string userName);
}

