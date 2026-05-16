using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctor;

public class StaffEntityConfiguration : IEntityTypeConfiguration<StaffEntity>
{
    public void Configure(EntityTypeBuilder<StaffEntity> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ──────────────────────────────────────────────────────────
        builder.ToTable("Staffs", "Finance", t => t.HasComment("হাসপাতালের নার্স, রিসেপশনিস্ট এবং অন্যান্য স্টাফদের প্রোফাইল টেবিল।"));

        builder.HasKey(s => s.Id);

        // স্টাফ কোড এবং ইউজার আইডি ইউনিক হওয়া উচিত
        builder.HasIndex(s => s.StaffCode).IsUnique();
        builder.HasIndex(s => s.ApplicationUserId).IsUnique();

        // ── ২. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(s => s.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.LastName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.StaffCode).IsRequired().HasMaxLength(20);
        builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(s => s.Email).IsRequired().HasMaxLength(150);
        builder.Property(s => s.NationalId).IsRequired().HasMaxLength(50);

        // স্যালারির জন্য প্রিসিশন
        builder.Property(s => s.Salary).HasPrecision(18, 2);

        // ── ৩. রিলেশনশিপ ──────────────────────────────────────────────────────────────

        // Department to Staff (One-to-Many)
        builder.HasOne(s => s.Department)
               .WithMany(d => d.Staff)
               .HasForeignKey(s => s.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict); // ডিপার্টমেন্ট ডিলিট করলে স্টাফদের ডাটা সেফ থাকবে

        // Staff to Attendance (One-to-Many)
        builder.HasMany(s => s.Attendances)
               .WithOne(a => a.Staff)
               .HasForeignKey(a => a.StaffId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}