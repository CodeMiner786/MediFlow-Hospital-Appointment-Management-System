using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.Configurations.Ambulance;

public class AmbulanceBookingConfiguration : IEntityTypeConfiguration<AmbulanceBooking>
{
    public void Configure(EntityTypeBuilder<AmbulanceBooking> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ───────────────────────────────────────────
        builder.ToTable("AmbulanceBookings", "Inventory", t => t.HasComment("অ্যাম্বুলেন্স বুকিং এবং এর বর্তমান অবস্থা ট্র্যাকিং টেবিল।"));

        // ── ২. প্রাইমারি কি ───────────────────────────────────────────────────
        builder.HasKey(b => b.Id);

        // ── ৩. প্রপার্টি কনফিগারেশন ──────────────────────────────────────
        builder.Property(b => b.BookingCode)
               .IsRequired()
               .HasMaxLength(50)
               .HasComment("বুকিং রেফারেন্স নম্বর যা ইউনিক হিসেবে ব্যবহৃত হয়।");

        builder.Property(b => b.RequesterName)
               .IsRequired()
               .HasMaxLength(60)
               .HasComment("যে ব্যক্তি অ্যাম্বুলেন্সটি কল করেছেন বা অর্ডার করেছেন।");

        builder.Property(b => b.RequesterPhone)
               .IsRequired()
               .HasMaxLength(20)
               .HasComment("রিকোয়েস্ট কারীর মোবাইল নম্বর।");

        builder.Property(b => b.PickupAddress)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("যেখান থেকে রোগীকে তোলা হবে।");

        builder.Property(b => b.DropAddress)
               .HasMaxLength(100)
               .HasComment("গন্তব্য বা হাসপাতালের ঠিকানা।");

        builder.Property(b => b.EmergencyDescription)
               .HasMaxLength(200)
               .HasComment("জরুরি অবস্থার সংক্ষিপ্ত বর্ণনা।");

        builder.Property(b => b.CancellationReason)
               .HasMaxLength(100)
               .HasComment("বুকিং বাতিল হলে তার কারণ।");

        // স্যালারি বা ভাড়ার জন্য প্রিসিশন
        builder.Property(b => b.Fare)
               .HasPrecision(18, 2)
               .HasComment("অ্যাম্বুলেন্স সার্ভিস চার্জ বা ভাড়া।");

        // ── ৪. রিলেশনশিপ কনফিগারেশন ──────────────────────────────────────────

        // ১. Vehicle এর সাথে (Many-to-One)
        builder.HasOne(b => b.Vehicle)
               .WithMany(v => v.Bookings)
               .HasForeignKey(b => b.VehicleId)
               .OnDelete(DeleteBehavior.Restrict);

        // ২. Provider এর সাথে (Many-to-One)
        builder.HasOne(b => b.Provider)
               .WithMany(p => p.Bookings)
               .HasForeignKey(b => b.ProviderId)
               .OnDelete(DeleteBehavior.Restrict);

        // ৩. Patient এর সাথে (Many-to-One, Optional)
        builder.HasOne(b => b.Patient)
               .WithMany()
               .HasForeignKey(b => b.PatientId)
               .OnDelete(DeleteBehavior.SetNull);

        // ── ৫. ইনডেক্সিং (পারফরম্যান্সের জন্য) ──────────────────────────────────
        builder.HasIndex(b => b.BookingCode).IsUnique();
        builder.HasIndex(b => b.RequesterPhone);
        builder.HasIndex(b => b.CreatedAt); // যদি BaseEntity তে থাকে
    }
}