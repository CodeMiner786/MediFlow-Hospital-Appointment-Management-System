using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.Account;
using HealthcareHospitalManagement.Domain.Enums.LoginWith;
using HealthcareHospitalManagement.Domain.Enums.UserRole;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HealthcareHospitalManagement.Infrastructure.Seeder
{
    public sealed class AdminSeederService(
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        ILogger<AdminSeederService> logger
    )
    {
        public async Task SeedAsync()
        {
            using var scope = serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            var email = configuration["AdminSeed:Email"];
            var password = configuration["AdminSeed:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                logger.LogWarning("⚠️ AdminSeed credentials পাওয়া যায়নি। appsettings.json চেক করো।");
                return;
            }

            // ✅ FIX: শুধু email দিয়ে check — role দিয়ে নয়
            var alreadyExists = await db.ApplicationUsers
                .AnyAsync(u => u.Email == email);

            if (alreadyExists)
            {
                logger.LogInformation("ℹ️ এই email দিয়ে user ইতিমধ্যে আছে। Seed skip।");
                return;
            }

            var superAdmin = new ApplicationUser
            {
                FirstName = configuration["AdminSeed:FirstName"] ?? "Super",
                LastName = configuration["AdminSeed:LastName"] ?? "Admin",
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                PhoneNumber = configuration["AdminSeed:PhoneNumber"] ?? "01700000000",

                PasswordHash = passwordHasher.Hash(password),
                PasswordSalt = null,

                Role = UserRole.SuperAdmin,
                AccountStatus = AccountStatus.Active,

                IsEmailVerified = true,
                EmailVerifiedAt = DateTime.UtcNow,
                IsPhoneVerified = true,
                PhoneVerifiedAt = DateTime.UtcNow,

                LoginProvider = LoginProvider.Local,

                CreatedByAdminId = null,
            };

            await db.ApplicationUsers.AddAsync(superAdmin);
            await db.SaveChangesAsync();

            logger.LogInformation("✅ SuperAdmin seed সফল! Email: {Email}", email);
        }
    }
}