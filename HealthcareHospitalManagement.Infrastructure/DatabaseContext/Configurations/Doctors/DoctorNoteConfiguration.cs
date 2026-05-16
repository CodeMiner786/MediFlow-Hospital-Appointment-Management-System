using HealthcareHospitalManagement.Domain.Entities.Doctor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorNoteConfiguration : IEntityTypeConfiguration<DoctorNote>
{
    public void Configure(EntityTypeBuilder<DoctorNote> builder)
    {
        // ── ১. টেবিল কনফিগারেশন এবং কমেন্ট ───────────────────────────────────────────────
        builder.ToTable("DoctorNotes", "Staff", t => t.HasComment("পেশেন্ট সম্পর্কে ডাক্তারদের ব্যক্তিগত বা শেয়ারড নোট রাখার টেবিল।"));

        // ── ২. কি এবং ইনডেক্স (Performance) ───────────────────────────────────────────
        builder.HasKey(n => n.Id);

        // নির্দিষ্ট পেশেন্ট বা ডাক্তারের নোট দ্রুত পাওয়ার জন্য ইনডেক্স
        builder.HasIndex(n => n.DoctorId);
        builder.HasIndex(n => n.PatientId);

        // ── ৩. কলাম কনফিগারেশন ─────────────────────────────────────────────────────────
        builder.Property(n => n.NoteTitle)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("নোটের শিরোনাম।");

        builder.Property(n => n.NoteContent)
            .IsRequired()
            .HasColumnType("nvarchar(max)")
            .HasComment("নোটের বিস্তারিত বর্ণনা।");

        builder.Property(n => n.Tags)
            .HasMaxLength(500)
            .HasComment("নোটের ট্যাগসমূহ (JSON ফরম্যাটে স্টোর করা যেতে পারে)।");

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────

        // Doctor to Notes
        builder.HasOne(n => n.Doctor)
               .WithMany(d => d.Notes)
               .HasForeignKey(n => n.DoctorId)
               .OnDelete(DeleteBehavior.Restrict); // ডাক্তার ডিলিট হলেও ক্লিনিক্যাল নোট থাকা উচিত

        // Patient to Notes
        builder.HasOne(n => n.Patient)
               .WithMany() // পেশেন্ট এনটিটিতে Notes কালেকশন থাকলে .WithMany(p => p.Notes) দিতে পারেন
               .HasForeignKey(n => n.PatientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}