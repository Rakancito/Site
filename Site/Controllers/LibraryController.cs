using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Controllers
{
    public class LibraryController : Controller
    {
        public static string MD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string GenerateMySQL5PasswordHash(string password)
        {
            return GenerateMySQL5PasswordHash(password, Encoding.UTF8);
        }

        public static string GenerateMySQL5PasswordHash(string password, Encoding textEncoding)
        {
            if (password == null)
                return null;
            byte[] passBytes = textEncoding.GetBytes(password);
            byte[] hash;
            using (var sha1 = SHA1.Create())
            {
                hash = sha1.ComputeHash(passBytes);
                hash = sha1.ComputeHash(hash);
            }

            var sb = new StringBuilder();
            sb.Append("*");
            for (int i = 0; i < hash.Length; i++)
                sb.AppendFormat("{0:X2}", hash[i]);

            return sb.ToString();
        }
    }
}
