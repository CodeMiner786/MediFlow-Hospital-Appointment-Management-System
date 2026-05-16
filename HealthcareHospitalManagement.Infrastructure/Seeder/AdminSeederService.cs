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
            // ✅ নতুন scope — DbContext এবং IPasswordHasher scoped service
            using var scope = serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            // ── appsettings.json থেকে credentials পড়া ────────────────
            var email = configuration["AdminSeed:Email"];
            var password = configuration["AdminSeed:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                logger.LogWarning("⚠️ AdminSeed credentials পাওয়া যায়নি। appsettings.json চেক করো।");
                return;
            }

            // ── আগে থেকে SuperAdmin আছে কিনা চেক ────────────────────
            var alreadyExists = await db.ApplicationUsers
                .AnyAsync(u => u.Email == email && u.Role == UserRole.SuperAdmin);

            if (alreadyExists)
            {
                logger.LogInformation("ℹ️ SuperAdmin ইতিমধ্যে বিদ্যমান। Seed skip।");
                return;
            }

            // ── নতুন SuperAdmin User তৈরি ────────────────────────────
            var superAdmin = new ApplicationUser
            {
                // ── Basic Info ────────────────────────────────────────
                FirstName = configuration["AdminSeed:FirstName"] ?? "Super",
                LastName = configuration["AdminSeed:LastName"] ?? "Admin",
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                PhoneNumber = configuration["AdminSeed:PhoneNumber"] ?? "01700000000",

                // ── Password ──────────────────────────────────────────
                // ✅ SHA256 দিয়ে hash — তোমার নিজের IPasswordHasher
                PasswordHash = passwordHasher.Hash(password),
                PasswordSalt = null,

                // ── Role & Status ─────────────────────────────────────
                // ✅ FIX 1: Admin → SuperAdmin — সর্বোচ্চ ক্ষমতার user
                Role = UserRole.SuperAdmin,
                AccountStatus = AccountStatus.Active,

                // ── Verification ──────────────────────────────────────
                IsEmailVerified = true,
                EmailVerifiedAt = DateTime.UtcNow,
                IsPhoneVerified = true,
                PhoneVerifiedAt = DateTime.UtcNow,

                // ── Login Provider ────────────────────────────────────
                LoginProvider = LoginProvider.Local,

                // ── Tracking ──────────────────────────────────────────
                CreatedByAdminId = null, // System তৈরি করছে
            };

            // ✅ FIX 2: SuperAdmin (undefined variable) → superAdmin (সঠিক variable)
            await db.ApplicationUsers.AddAsync(superAdmin);
            await db.SaveChangesAsync();

            logger.LogInformation("✅ SuperAdmin seed সফল! Email: {Email}", email);
        }
    }
}