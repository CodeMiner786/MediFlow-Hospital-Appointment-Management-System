using System.Text.Json;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.Rating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Configurations.Doctors;

public class DoctorFeedbackSummaryConfiguration : IEntityTypeConfiguration<DoctorFeedbackSummary>
{
    // ✅ FIX 5 (Static Readonly): JsonSerializerOptions একবার তৈরি করলেই হয়, বারবার new করার দরকার নেই
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.General);

    public void Configure(EntityTypeBuilder<DoctorFeedbackSummary> builder)
    {
        // ── ১. টেবিল কনফিগারেশন ──────────────────────────────────────────────────────────
        builder.ToTable("DoctorFeedbackSummaries", "Staff", t =>
            t.HasComment("ডাক্তারদের গড় রেটিং এবং রিভিউ গণনার সারসংক্ষেপ টেবিল।"));

        builder.HasKey(s => s.Id);

        // প্রতিটি ডাক্তারের একটিই summary থাকবে (1:1)
        builder.HasIndex(s => s.DoctorId).IsUnique();

        // ── ২. ডেসিমাল প্রিসিশন ─────────────────────────────────────────────────────────
        // ✅ FIX 4: AverageRating এ precision নির্ধারণ (যেমন: 4.95 → precision 3, scale 2)
        builder.Property(s => s.AverageRating).HasPrecision(3, 2);

        // ── ৩. JSON Conversion এবং Value Comparer ────────────────────────────────────────
        // ✅ FIX 2 (Value Comparer সমাধান):
        // Dictionary<RatingStar, int> হলো একটি reference type collection।
        // EF Core এর change tracker এর পক্ষে এর ভেতরের পরিবর্তন সনাক্ত করা সম্ভব নয়।
        // তাই ValueComparer দিতে হবে যা EF Core-কে বলে দেয়:
        //   ১. দুটো Dictionary সমান কিনা কীভাবে বুঝবে (Equals)
        //   ২. hash code কীভাবে তৈরি হবে (GetHashCode)
        //   ৩. snapshot/clone কীভাবে নেবে (Snapshot)

        var ratingCountsComparer = new ValueComparer<Dictionary<RatingStar, int>>(
            // Equals: JSON string compare করে দুটো dictionary তুলনা
            (c1, c2) => JsonSerializer.Serialize(c1, JsonOptions) == JsonSerializer.Serialize(c2, JsonOptions),
            // GetHashCode: JSON string থেকে hash নেওয়া
            c => c == null ? 0 : JsonSerializer.Serialize(c, JsonOptions).GetHashCode(),
            // Snapshot: deep copy — এটি ছাড়া update ট্র্যাক হবে না
            c => JsonSerializer.Deserialize<Dictionary<RatingStar, int>>(
                     JsonSerializer.Serialize(c, JsonOptions), JsonOptions) ?? new Dictionary<RatingStar, int>()
        );

        builder.Property(s => s.RatingCounts)
               .HasConversion(
                   // DB তে store হওয়ার সময়: Dictionary → JSON string
                   v => JsonSerializer.Serialize(v, JsonOptions),
                   // DB থেকে পড়ার সময়: JSON string → Dictionary
                   v => JsonSerializer.Deserialize<Dictionary<RatingStar, int>>(v, JsonOptions)
                        ?? new Dictionary<RatingStar, int>()
               )
               .HasColumnType("nvarchar(max)")
               .HasComment("৫-স্টার থেকে ১-স্টার রেটিংয়ের সংখ্যাসমূহ JSON ফরম্যাটে।");

        // ✅ ValueComparer আলাদাভাবে set করতে হবে — HasConversion এর সাথে chain করা যায় না
        builder.Property(s => s.RatingCounts)
               .Metadata.SetValueComparer(ratingCountsComparer);

        // ── ৪. রিলেশনশিপ ──────────────────────────────────────────────────────────────────
        // DoctorEntity তে FeedbackSummary navigation prop আছে (1:1)
        builder.HasOne(s => s.Doctor)
               .WithOne(d => d.FeedbackSummary)
               .HasForeignKey<DoctorFeedbackSummary>(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
