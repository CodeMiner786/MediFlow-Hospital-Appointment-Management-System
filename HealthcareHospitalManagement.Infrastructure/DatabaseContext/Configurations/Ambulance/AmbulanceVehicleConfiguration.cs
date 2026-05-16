using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ফোল্ডার স্ট্রাকচার অনুযায়ী সঠিক নেমস্পেস
namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Ambulance;

public class AmbulanceVehicleConfiguration : IEntityTypeConfiguration<AmbulanceVehicle>
{
    public void Configure(EntityTypeBuilder<AmbulanceVehicle> builder)
    {
        // ── ১. টেবিল ও স্কিমা কনফিগারেশন ──────────────────────────────────────────────────
        builder.ToTable("AmbulanceVehicles", "Inventory", t => t.HasComment("অ্যাম্বুলেন্স প্রোভাইডারদের অধীনে থাকা গাড়িসমূহের তথ্য।"));

        // ── ২. প্রাইমারি কি ─────────────────────────────────────────────────────
        builder.HasKey(v => v.Id);

        // ── ৩. ইনডেক্সিং (Unique Indexes) ──────────────────────────────────────────────────
        // গাড়ির নাম্বার এবং অ্যাম্বুলেন্স কোড অবশ্যই ইউনিক হতে হবে দ্রুত সার্চের জন্য
        builder.HasIndex(v => v.VehicleNumber).IsUnique();
        builder.HasIndex(v => v.AmbulanceCode).IsUnique();

        // ── ৪. প্রপার্টি কনফিগারেশন ────────────────────────────────────────
        builder.Property(v => v.VehicleNumber)
               .IsRequired()
               .HasMaxLength(30)
               .HasComment("গাড়ির লাইসেন্স প্লেট বা রেজিস্ট্রেশন নম্বর।");

        builder.Property(v => v.AmbulanceCode)
               .IsRequired()
               .HasMaxLength(20)
               .HasComment("অভ্যন্তরীণ চেনার জন্য বিশেষ কোড (যেমন: AMB-001)।");

        builder.Property(v => v.DriverName)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("গাড়িচালকের নাম।");

        builder.Property(v => v.DriverPhone)
               .IsRequired()
               .HasMaxLength(20)
               .HasComment("গাড়িচালকের মোবাইল নম্বর।");

        builder.Property(v => v.ParamedicName)
               .HasMaxLength(100)
               .HasComment("গাড়িতে উপস্থিত প্যারামেডিক বা সহকারীর নাম (ঐচ্ছিক)।");

        builder.Property(v => v.ParamedicPhone)
               .HasMaxLength(20);

        builder.Property(v => v.Notes)
               .HasMaxLength(500)
               .HasComment("গাড়ি সংক্রান্ত বিশেষ কোনো নোট।");

        // ফোরেন কি প্রপার্টিতে কমেন্ট
        builder.Property(v => v.ProviderId)
               .HasComment("এই গাড়িটি কোন প্রোভাইডারের তার রেফারেন্স।");

        // ── ৫. রিলেশনশিপ কনফিগারেশন ───────────────────────────────────────────────────

        // ১. Vehicle to Provider (Many-to-One)
        builder.HasOne(v => v.Provider)
               .WithMany(p => p.Vehicles)
               .HasForeignKey(v => v.ProviderId)
               .OnDelete(DeleteBehavior.Restrict);

        // ২. Vehicle to Bookings (One-to-Many)
        builder.HasMany(v => v.Bookings)
               .WithOne(b => b.Vehicle)
               .HasForeignKey(b => b.VehicleId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}