using System.Security.Cryptography;
using System.Text;

namespace BrazilianAddresses.Application.Services.Cryptography
{
    public class PasswordEncryption
    {
        public string Encrypt(string password, string salt)
        {
            string passwordWithSalt = $"{password}{salt}";

            byte[] bytes = Encoding.UTF8.GetBytes(passwordWithSalt);
            
            byte[] hashBytes = SHA512.HashData(bytes);

            return StringBytes(hashBytes);
        }

        private static string StringBytes(byte[] hashBytes)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hashBytes)
            {
                string hex = b.ToString("x2");
                stringBuilder.Append(hex);
            }

            return stringBuilder.ToString();
        }
    }
}
