using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        string HashPassword(string password);
        bool Verify(string currentPassword, string passwordHash);
        bool VerifyPassword(string password, string hash);
    }
}
