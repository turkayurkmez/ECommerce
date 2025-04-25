using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Infrastructure.Services
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPasssword);
    }

    public class PasswordHashingService : IPasswordHashingService
    {
        public string HashPassword(string password)
        {
            // Implement your password hashing logic here
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string hashedPassword, string providedPasssword)
        {
            // Implement your password verification logic here
            return BCrypt.Net.BCrypt.Verify(providedPasssword, hashedPassword);
        }
    }
}
