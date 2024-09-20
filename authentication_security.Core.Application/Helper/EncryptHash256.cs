using System.Security.Cryptography;
using System.Text;

namespace authentication_security.Core.Application.Helper
{
    public class EncryptHash256
    {
        public static string EncryptPassword(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
