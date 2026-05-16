using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("Departments", "Staff", t => t.HasComment("হসপিটালের বিভিন্ন মেডিকেল ডিপার্টমেন্ট (যেমন: কার্ডিওলজি, ডার্মাটোলজি) এর তালিকা।"));

        // ── ২. কি এবং ইউনিক ইনডেক্স ────────────────────────────────────────────────────
        builder.HasKey(d => d.Id);

        // একই নামের বা কোডের ডিপার্টমেন্ট যেন ডুপ্লিকেট না হয়
        builder.HasIndex(d => d.Name).IsUnique();
        builder.HasIndex(d => d.Code).IsUnique();

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Code).IsRequired().HasMaxLength(20);
        builder.Property(d => d.Description).HasMaxLength(500);
        builder.Property(d => d.Location).HasMaxLength(200);
        builder.Property(d => d.HeadDoctorName).HasMaxLength(150);
        builder.Property(d => d.ContactExtension).HasMaxLength(20);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────

        // Department to Doctors (One-to-Many)
        builder.HasMany(d => d.Doctors)
               .WithOne(doc => doc.Department)
               .HasForeignKey(doc => doc.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict); // ডিপার্টমেন্টে ডক্টর থাকলে ডিপার্টমেন্ট ডিলিট করা যাবে না

        // Department to Staff (One-to-Many)
        builder.HasMany(d => d.Staff)
               .WithOne(s => s.Department)
               .HasForeignKey(s => s.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}