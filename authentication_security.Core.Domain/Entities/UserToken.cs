
namespace authentication_security.Core.Domain.Entities;

public class UserToken : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public bool IsValid { get; set; } = true;
    public DateTime StartSession { get; set; } = DateTime.Now;
    public DateTime EndSession { get; set; } = DateTime.Now;
    public string TimeInSession { get; set; } = string.Empty;

}