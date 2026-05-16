using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using System.Security.Cryptography;
using System.Text;

namespace HealthcareHospitalManagement.Infrastructure.ServiceExtensions.JwtService
{
    public class PasswordHasher : IPasswordHasher
    {
        // ১. Hash মেথড ইমপ্লিমেন্টেশন
        public string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // ২. HashPassword মেথড (এটি সরাসরি Hash মেথডকে কল করছে কোড ডুপ্লিকেশন এড়াতে)
        public string HashPassword(string password)
            => Hash(password);

        // ৩. Verify মেথড ইমপ্লিমেন্টেশন
        public bool Verify(string currentPassword, string passwordHash)
            => Hash(currentPassword) == passwordHash;

        // ৪. VerifyPassword মেথড (এটি সরাসরি Verify মেথডকে কল করছে)
        public bool VerifyPassword(string password, string hash)
            => Verify(password, hash);
    }
}