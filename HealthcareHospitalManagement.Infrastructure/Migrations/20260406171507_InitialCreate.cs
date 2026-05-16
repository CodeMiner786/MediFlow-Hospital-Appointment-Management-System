using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareHospitalManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Finance");

            migrationBuilder.EnsureSchema(
                name: "Ward");

            migrationBuilder.EnsureSchema(
                name: "Inventory");

            migrationBuilder.EnsureSchema(
                name: "Patient");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.EnsureSchema(
                name: "Social");

            migrationBuilder.EnsureSchema(
                name: "Staff");

            migrationBuilder.EnsureSchema(
                name: "Patients");

            migrationBuilder.CreateTable(
                name: "AdminDashboardMetrics",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetricDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalRegisteredUsers = table.Column<int>(type: "int", nullable: false),
                    NewUsersToday = table.Column<int>(type: "int", nullable: false),
                    ActiveDoctors = table.Column<int>(type: "int", nullable: false),
                    ActiveAmbulanceProviders = table.Column<int>(type: "int", nullable: false),
                    ActiveLabProfiles = table.Column<int>(type: "int", nullable: false),
                    ActivePharmacies = table.Column<int>(type: "int", nullable: false),
                    PendingVerifications = table.Column<int>(type: "int", nullable: false),
                    TotalAppointmentsToday = table.Column<int>(type: "int", nullable: false),
                    TotalAmbulanceBookingsToday = table.Column<int>(type: "int", nullable: false),
                    TotalLabOrdersToday = table.Column<int>(type: "int", nullable: false),
                    TotalMedicineOrdersToday = table.Column<int>(type: "int", nullable: false),
                    TotalBedBookingsToday = table.Column<int>(type: "int", nullable: false),
                    PlatformRevenueToday = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DoctorRevenueToday = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LabRevenueToday = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PharmacyRevenueToday = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BedBookingRevenueToday = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyPlatformRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmbulanceWalletBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAuditLogsToday = table.Column<int>(type: "int", nullable: false),
                    ActiveConversationsToday = table.Column<int>(type: "int", nullable: false),
                    LastReportGeneratedAt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastReportStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminDashboardMetrics", x => x.Id);
                },
                comment: "সুপার অ্যাডমিন ড্যাশবোর্ডের জন্য প্রতিদিনের পরিসংখ্যানের স্ন্যাপশট।");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                },
                comment: "সিস্টেমের সকল সেনসিটিভ ডাটা পরিবর্তনের রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "DashboardMetrics",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetricDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "যে তারিখের জন্য এই পরিসংখ্যান তৈরি করা হয়েছে।"),
                    TotalPatients = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "ঐ দিনে মোট সেবা নেওয়া রোগীর সংখ্যা।"),
                    NewPatients = table.Column<int>(type: "int", nullable: false),
                    OpdVisits = table.Column<int>(type: "int", nullable: false),
                    IpdAdmissions = table.Column<int>(type: "int", nullable: false),
                    Discharges = table.Column<int>(type: "int", nullable: false),
                    TotalAppointments = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "ঐ দিনের মোট অ্যাপয়েন্টমেন্ট সংখ্যা।"),
                    CompletedAppointments = table.Column<int>(type: "int", nullable: false),
                    CancelledAppointments = table.Column<int>(type: "int", nullable: false),
                    TelemedicineSessions = table.Column<int>(type: "int", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "ঐ দিনের মোট সর্বমোট আয়।"),
                    DoctorRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "ডক্টর অ্যাপয়েন্টমেন্ট থেকে অর্জিত আয়।"),
                    PharmacyRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "ফার্মেসী বা ঔষধ বিক্রি থেকে অর্জিত আয়।"),
                    LabRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "ল্যাব টেস্ট বা প্যাথলজি থেকে অর্জিত আয়।"),
                    AmbulanceRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "অ্যাম্বুলেন্স সার্ভিস থেকে অর্জিত আয়।"),
                    BedBookingRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "বেড বা ওয়ার্ড বুকিং থেকে অর্জিত আয়।"),
                    PendingDues = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "ঐ দিনের বকেয়া বা পাওনা টাকার পরিমাণ।"),
                    AmbulanceBookings = table.Column<int>(type: "int", nullable: false),
                    AmbulanceAvailable = table.Column<int>(type: "int", nullable: false),
                    LabTestsOrdered = table.Column<int>(type: "int", nullable: false),
                    LabTestsCompleted = table.Column<int>(type: "int", nullable: false),
                    ActiveDoctors = table.Column<int>(type: "int", nullable: false),
                    ActiveAmbulanceProviders = table.Column<int>(type: "int", nullable: false),
                    ActiveLabProfiles = table.Column<int>(type: "int", nullable: false),
                    ActivePharmacies = table.Column<int>(type: "int", nullable: false),
                    TotalRegisteredUsers = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "সিস্টেমে মোট নিবন্ধিত ইউজারের সংখ্যা।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardMetrics", x => x.Id);
                },
                comment: "হাসপাতালের প্রতিদিনের আয়, রোগী এবং অন্যান্য গুরুত্বপূর্ণ পরিসংখ্যানের ডেটা টেবিল।");

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HeadDoctorName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ContactExtension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                },
                comment: "হসপিটালের বিভিন্ন মেডিকেল ডিপার্টমেন্ট (যেমন: কার্ডিওলজি, ডার্মাটোলজি) এর তালিকা।");

            migrationBuilder.CreateTable(
                name: "HospitalSettings",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultPlatformSharePercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "হাসপাতালের ডিফল্ট কমিশন শতাংশ।"),
                    DefaultDoctorSharePercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "ডাক্তারের ডিফল্ট আয়ের শতাংশ।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalSettings", x => x.Id);
                },
                comment: "হাসপাতালের গ্লোবাল কনফিগারেশন এবং রেভিনিউ শেয়ারিং পার্সেন্টেজ টেবিল।");

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BodyEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectBn = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    BodyBn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                },
                comment: "ইমেইল, এসএমএস এবং পুশ নোটিফিকেশনের ডাইনামিক টেমপ্লেট স্টোর।");

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "পেশেন্টের ইউনিক আইডেন্টিফিকেশন কোড।"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BloodGroup = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "জাতীয় পরিচয়পত্র বা জন্ম নিবন্ধন নম্বর।"),
                    PatientType = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlternatePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactRelation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "পেশেন্টের ওজন (কেজি)।"),
                    HeightCm = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "পেশেন্টের উচ্চতা (সেমি)।"),
                    BMI = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "বডি মাস ইনডেক্স।"),
                    Allergies = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ChronicDiseases = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CurrentMedications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastSurgeries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyMedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasInsurance = table.Column<bool>(type: "bit", nullable: false),
                    InsuranceProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsurancePolicyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                },
                comment: "পেশেন্টদের ব্যক্তিগত, কন্টাক্ট এবং মেডিকেল প্রোফাইল সংরক্ষণের মূল টেবিল।");

            migrationBuilder.CreateTable(
                name: "PlatformWallets",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalReceived = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalRefunded = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    LastTransactionAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformWallets", x => x.Id);
                },
                comment: "প্ল্যাটফর্মের কেন্দ্রীয় ওয়ালেট যেখানে সব রেভিনিউ জমা হয়।");

            migrationBuilder.CreateTable(
                name: "ReportEntities",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "রিপোর্টের নাম (যেমন: Annual Financial Report 2026)"),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneratedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "যে অ্যাডমিন রিপোর্টটি তৈরি করেছেন তার ইউজারনেম বা আইডি"),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "রিপোর্টের মূল ডাটাগুলোর একটি JSON সামারি (দ্রুত প্রিভিউয়ের জন্য)"),
                    PdfUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "জেনারেট হওয়া PDF ফাইলের পাথ বা URL।"),
                    ExcelUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "জেনারেট হওয়া Excel ফাইলের পাথ বা URL।"),
                    WordUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "জেনারেট হওয়া Word ফাইলের পাথ বা URL।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportEntities", x => x.Id);
                },
                comment: "সুপার অ্যাডমিন কর্তৃক জেনারেট করা বার্ষিক বা মাসিক রিপোর্টের আর্কাইভ এবং ফাইল লিংক স্টোর করে।");

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "মডিউলের নাম (যেমন: Billing, Pharmacy, Patient)"),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsGranted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                },
                comment: "বিভিন্ন ইউজার রোলের জন্য মডিউল ভিত্তিক পারমিশন সেটিংস।");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                },
                comment: "ওষুধ সরবরাহকারী বা ভেন্ডরদের তথ্য।");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LinkedProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPhoneVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsTwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorSecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    LockoutEndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginProvider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExternalProviderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                },
                comment: "সিস্টেমের সকল ইউজার এবং তাদের সিকিউরিটি ক্রেডেনশিয়াল।");

            migrationBuilder.CreateTable(
                name: "Wards",
                schema: "Ward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WardName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WardCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WardType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Building = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalBeds = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AvailableBeds = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InchargeNurseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Id);
                },
                comment: "হাসপাতালের বিভিন্ন ওয়ার্ড বা ডিপার্টমেন্টের তথ্য।");

            migrationBuilder.CreateTable(
                name: "Doctors",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Specialization = table.Column<int>(type: "int", nullable: false),
                    SubSpecialization = table.Column<int>(type: "int", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LicenseExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    Qualifications = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ConsultationFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TelemedicineConsultationFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    HomeVisitFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    CustomDoctorSharePercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    AvailabilityStatus = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    OfficeExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalReviews = table.Column<int>(type: "int", nullable: false),
                    TotalAppointments = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Staff",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "সিস্টেমের সকল ডাক্তারদের মূল প্রোফাইল এবং প্রফেশনাল ইনফরমেশন টেবিল।");

            migrationBuilder.CreateTable(
                name: "Staffs",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StaffType = table.Column<int>(type: "int", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Staff",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "হাসপাতালের নার্স, রিসেপশনিস্ট এবং অন্যান্য স্টাফদের প্রোফাইল টেবিল।");

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedbackType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Pending"),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmbulanceProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LabProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PharmacyProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false, comment: "সেবার মান রেটিং (১ থেকে ৫)।"),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "পেশেন্টের দেওয়া বিস্তারিত মন্তব্য।"),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    AdminResponse = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "কর্তৃপক্ষের পক্ষ থেকে দেওয়া উত্তর।"),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RespondedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পেশেন্টদের পক্ষ থেকে বিভিন্ন সেবার ফিডব্যাক ও রেটিং।");

            migrationBuilder.CreateTable(
                name: "HealthLogs",
                schema: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "লগটি কোন পেশেন্টের, তার ইউনিক আইডি।"),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "স্বাস্থ্য তথ্যটি ঠিক কখন রেকর্ড করা হয়েছে সেই সময়।"),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "ডাটা সোর্স: Manual, Smartwatch, BPMonitor ইত্যাদি।"),
                    WeightKg = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "পেশেন্টের ওজন (Kg), যেমন: ৭০.৫০ কেজি।"),
                    HeightCm = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "পেশেন্টের উচ্চতা (Cm), যেমন: ১৭০.২৫ সেমি।"),
                    BloodPressureSystolic = table.Column<int>(type: "int", nullable: true, comment: "সিস্টোলিক ব্লাড প্রেসার (উপরের রিডিং), যেমন: ১২০।"),
                    BloodPressureDiastolic = table.Column<int>(type: "int", nullable: true, comment: "ডায়াস্টোলিক ব্লাড প্রেসার (নিচের রিডিং), যেমন: ৮০।"),
                    BloodGlucose = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "রক্তে শর্করার মাত্রা (Blood Glucose Level)।"),
                    HeartRate = table.Column<int>(type: "int", nullable: true, comment: "হার্ট রেট বা হৃদস্পন্দনের গতি (BPM)।"),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "রক্তে অক্সিজেনের মাত্রা (SpO2 %)।"),
                    TemperatureCelsius = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "শরীরের তাপমাত্রা (Celsius), যেমন: ৯৮.৬০।"),
                    StepsCount = table.Column<int>(type: "int", nullable: true, comment: "সারাদিনে মোট কত কদম বা স্টেপস হেঁটেছেন।"),
                    SleepHours = table.Column<int>(type: "int", nullable: true, comment: "গত ২৪ ঘণ্টায় ঘুমের সময় (ঘণ্টায়)।"),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "লগ সম্পর্কিত অতিরিক্ত কোনো নোট বা মন্তব্য।"),
                    DeviceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "যদি কোনো ডিভাইস থেকে ডাটা আসে, তার ইউনিক আইডি।"),
                    DeviceModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "ডিভাইসটির মডেলের নাম।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthLogs_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পেশেন্টদের দৈনন্দিন স্বাস্থ্য সংক্রান্ত লগ (যেমন: ওজন, সুগার, স্টেপস) সংরক্ষণের টেবিল।");

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "বিলের বিপরীতে জেনারেট হওয়া ফাইনাল ইনভয়েস রেকর্ড এবং পিডিএফ ট্র্যাকিং টেবিল।");

            migrationBuilder.CreateTable(
                name: "PatientVitals",
                schema: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "ভাইটালটি রেকর্ড করার সময়।"),
                    RecordedByName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "যিনি ভাইটালটি রেকর্ড করেছেন (নার্স বা ডাক্তার)।"),
                    TemperatureCelsius = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "শরীরের তাপমাত্রা (Celsius), যেমন: ৩৭.৫০।"),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    BloodPressureSystolic = table.Column<int>(type: "int", nullable: true),
                    BloodPressureDiastolic = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "অক্সিজেন লেভেল (SpO2 %)।"),
                    WeightKg = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "ওজন (কেজি), যেমন: ৭০.৫০।"),
                    HeightCm = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "উচ্চতা (সেমি), যেমন: ১৭৫.০০।"),
                    BMI = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "বডি মাস ইনডেক্স (BMI)।"),
                    BloodGlucose = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true, comment: "রক্তে শর্করার পরিমাণ।"),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "অতিরিক্ত কোনো পর্যবেক্ষণ।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVitals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পেশেন্টদের শারীরিক ভাইটাল প্যারামিটারসমূহ (রক্তচাপ, সুগার ইত্যাদি) সংরক্ষণের টেবিল।");

            migrationBuilder.CreateTable(
                name: "AmbulanceProviderProfiles",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "সিস্টেম ইউজার অ্যাকাউন্টের সাথে এই প্রোফাইলের লিংক।"),
                    ProviderName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "প্রোভাইডার বা কোম্পানির নাম।"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "সরকারি বা ট্রেড লাইসেন্স রেজিস্ট্রেশন নম্বর।"),
                    ContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "অফিসিয়াল যোগাযোগের নম্বর।"),
                    EmergencyHotline = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "জরুরি অ্যাম্বুলেন্স কলের জন্য হটলাইন নম্বর।"),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "প্রোভাইডারের অফিসের পূর্ণ ঠিকানা।"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false, comment: "ইউজারদের দেওয়া গড় রেটিং (যেমন: ৪.৫৫)।"),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceProviderProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceProviderProfiles_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "অ্যাম্বুলেন্স সেবাদানকারী প্রতিষ্ঠান বা প্রোভাইডারদের প্রোফাইল তথ্য।");

            migrationBuilder.CreateTable(
                name: "Conversations",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParticipantAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantAType = table.Column<int>(type: "int", nullable: false),
                    ParticipantBId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantBType = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferenceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastMessageAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastMessagePreview = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_ParticipantAId",
                        column: x => x.ParticipantAId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_ParticipantBId",
                        column: x => x.ParticipantBId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ইউজারদের মধ্যকার চ্যাট থ্রেড বা কনভারসেশন লিস্ট।");

            migrationBuilder.CreateTable(
                name: "FeedItems",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "ফিড আইটেমের মূল শিরোনাম।"),
                    SubTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "আইটেমটি যদি কোনো নির্দিষ্ট শহরের জন্য প্রাসঙ্গিক হয়।"),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    PrimaryAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAvailableNow = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItems_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "অ্যাপের মেইন ফিড বা টাইমলাইনে দেখানোর মতো আইটেমগুলোর তথ্য।");

            migrationBuilder.CreateTable(
                name: "LabProfiles",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabProfiles_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ল্যাব ডিপার্টমেন্টের প্রোফাইল এবং কন্টাক্ট ইনফরমেশন।");

            migrationBuilder.CreateTable(
                name: "OtpCodes",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "সিকিউরিটির জন্য ওটিপি কোডটি হ্যাশ করে সেভ করা হয়েছে।"),
                    Purpose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SentTo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "ওটিপিটি কোন ইমেইল বা ফোন নম্বরে পাঠানো হয়েছে।"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    MaxAttempts = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtpCodes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজার ভেরিফিকেশন এবং পাসওয়ার্ড রিসেটের জন্য ব্যবহৃত ওটিপি রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "PasswordResetRequests",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "নিরাপত্তার জন্য রিসেট টোকেনটি হ্যাশ করে রাখা হয়েছে।"),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetRequests_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারদের পাসওয়ার্ড রিসেট করার টোকেন ও রিকোয়েস্ট ট্র্যাকিং।");

            migrationBuilder.CreateTable(
                name: "PharmacyProfiles",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    HasDeliveryService = table.Column<bool>(type: "bit", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacyProfiles_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ফার্মেসির প্রোফাইল এবং লাইসেন্স সংক্রান্ত তথ্য।");

            migrationBuilder.CreateTable(
                name: "RoleAssignmentLogs",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedByEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AssignedByRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAssignmentLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAssignmentLogs_Users_AssignedByUserId",
                        column: x => x.AssignedByUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleAssignmentLogs_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারদের রোল পরিবর্তনের ইতিহাস ও কারণ ট্র্যাকিং।");

            migrationBuilder.CreateTable(
                name: "UserLoginHistories",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    FailureReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoginProvider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeviceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OperatingSystem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLoginHistories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারদের লগইন করার বিস্তারিত ইতিহাস ও ডিভাইস তথ্য।");

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TitleBn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MessageBn = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IconType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferenceType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEmailSent = table.Column<bool>(type: "bit", nullable: false),
                    EmailSentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSmsSent = table.Column<bool>(type: "bit", nullable: false),
                    SmsSentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPushSent = table.Column<bool>(type: "bit", nullable: false),
                    PushSentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারদের জন্য ইন-অ্যাপ, ইমেইল এবং এসএমএস নোটিফিকেশন রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "UserPermissionOverrides",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "মডিউলের নাম (যেমন: Billing, Inventory)"),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsGranted = table.Column<bool>(type: "bit", nullable: false),
                    GrantedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GrantedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissionOverrides_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারের জন্য রোলের বাইরে আলাদাভাবে পারমিশন সেট করার রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "র্যান্ডমলি জেনারেটেড সিকিউর রিফ্রেশ টোকেন।"),
                    JwtId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "এক্সেস টোকেনের সাথে এই রিফ্রেশ টোকেনকে ম্যাপ করার জন্য JWT ID।"),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারদের লগইন সেশন সচল রাখার জন্য রিফ্রেশ টোকেন স্টোর।");

            migrationBuilder.CreateTable(
                name: "Beds",
                schema: "Ward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BedNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DailyCharge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HasOxygen = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HasMonitor = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsIsolation = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    WardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beds_Wards_WardId",
                        column: x => x.WardId,
                        principalSchema: "Ward",
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "হাসপাতালের ওয়ার্ডের অন্তর্গত প্রতিটি বেড এবং তার বর্তমান অবস্থার তথ্য।");

            migrationBuilder.CreateTable(
                name: "DoctorAvailabilityLogs",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpectedBackAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DoctorEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorAvailabilityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilityLogs_Doctors_DoctorEntityId",
                        column: x => x.DoctorEntityId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DoctorAvailabilityLogs_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের অনলাইন/অফলাইন বা অন-ডিউটি স্ট্যাটাস পরিবর্তনের ইতিহাস রাখার টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorDashboardMetrics",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetricDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalAppointmentsToday = table.Column<int>(type: "int", nullable: false),
                    CompletedAppointments = table.Column<int>(type: "int", nullable: false),
                    PendingAppointments = table.Column<int>(type: "int", nullable: false),
                    CancelledAppointments = table.Column<int>(type: "int", nullable: false),
                    NoShowAppointments = table.Column<int>(type: "int", nullable: false),
                    TelemedicineAppointments = table.Column<int>(type: "int", nullable: false),
                    TotalPatientsToday = table.Column<int>(type: "int", nullable: false),
                    NewPatientsToday = table.Column<int>(type: "int", nullable: false),
                    FollowUpPatients = table.Column<int>(type: "int", nullable: false),
                    TotalRevenueToday = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ConsultationRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TelemedicineRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PendingPayments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyAppointments = table.Column<int>(type: "int", nullable: false),
                    MonthlyRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalReviews = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDashboardMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorDashboardMetrics_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের ব্যক্তিগত ড্যাশবোর্ড এবং দৈনিক আয়ের পরিসংখ্যান টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorDocuments",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorDocuments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের প্রফেশনাল সার্টিফিকেট এবং ভেরিফিকেশন ডকুমেন্টের তালিকা।");

            migrationBuilder.CreateTable(
                name: "DoctorEarnings",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelemedicineSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EarningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    HospitalSharePercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    HospitalShareAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DoctorShareAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EarningType = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorEarnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorEarnings_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ডাক্তারদের প্রতিটি অ্যাপয়েন্টমেন্ট বা সেশনের আয়ের বিস্তারিত হিসাব এবং হসপিটাল শেয়ার ট্র্যাকিং টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorFeedbackSummaries",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalReviews = table.Column<int>(type: "int", nullable: false),
                    RatingCounts = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "৫-স্টার থেকে ১-স্টার রেটিংয়ের সংখ্যাসমূহ JSON ফরম্যাটে।"),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorFeedbackSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorFeedbackSummaries_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের গড় রেটিং এবং রিভিউ গণনার সারসংক্ষেপ টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorLeaves",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "ছুটি নেওয়ার কারণ।"),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true, comment: "যিনি ছুটি অনুমোদন করেছেন (অ্যাডমিন আইডি বা নাম)।"),
                    RejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "আবেদন বাতিল হলে তার কারণ।"),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "অতিরিক্ত কোনো মন্তব্য বা বিশেষ নোট।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorLeaves_Doctors",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের ছুটির আবেদন এবং অনুমোদনের রেকর্ড রাখার টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorNotes",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NoteTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "নোটের শিরোনাম।"),
                    NoteContent = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "নোটের বিস্তারিত বর্ণনা।"),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "নোটের ট্যাগসমূহ (JSON ফরম্যাটে স্টোর করা যেতে পারে)।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorNotes_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorNotes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পেশেন্ট সম্পর্কে ডাক্তারদের ব্যক্তিগত বা শেয়ারড নোট রাখার টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorNotifications",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "নোটিফিকেশনের শিরোনাম।"),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "নোটিফিকেশনের বিস্তারিত বার্তা।"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferenceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "যে এনটিটির রেফারেন্সে নোটিফিকেশনটি তৈরি হয়েছে।"),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "নোটিফিকেশনে ক্লিক করলে যে লিংকে রিডাইরেক্ট করবে।"),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorNotifications_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের জন্য ইন-অ্যাপ নোটিফিকেশন এবং অ্যালার্ট স্টোর করার টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorPerformanceReports",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TotalAppointments = table.Column<int>(type: "int", nullable: false),
                    CompletedAppointments = table.Column<int>(type: "int", nullable: false),
                    CancelledAppointments = table.Column<int>(type: "int", nullable: false),
                    NoShows = table.Column<int>(type: "int", nullable: false),
                    CompletionRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    TotalPatientsSeen = table.Column<int>(type: "int", nullable: false),
                    NewPatients = table.Column<int>(type: "int", nullable: false),
                    ReturnPatients = table.Column<int>(type: "int", nullable: false),
                    AverageConsultationMinutes = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ConsultationRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TelemedicineRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PlatformShareAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DoctorShareAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalReviews = table.Column<int>(type: "int", nullable: false),
                    FiveStarReviews = table.Column<int>(type: "int", nullable: false),
                    OneStarReviews = table.Column<int>(type: "int", nullable: false),
                    GeneratedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PdfUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPerformanceReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorPerformanceReports_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের পারফরম্যান্স এবং রেভিনিউ সংক্রান্ত বিস্তারিত রিপোর্ট টেবিল।");

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ShiftType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaxAppointments = table.Column<int>(type: "int", nullable: false),
                    SlotDurationMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 15),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "ডাক্তার যেখানে বসবেন (উদা: Room 402, Building A)।"),
                    IsTelemedicineSlot = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SetBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের সাপ্তাহিক ডিউটি শিডিউল এবং কনসালটেশন সেটিংস।");

            migrationBuilder.CreateTable(
                name: "DoctorUnavailabilities",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnavailableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    ToTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "অনুপস্থিতির কারণ (উদা: লাঞ্চ ব্রেক, পার্সোনাল কাজ)।"),
                    IsFullDay = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorUnavailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorUnavailabilities_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ডাক্তারদের সাময়িক অনুপস্থিতি বা ব্রেক রেকর্ড করার টেবিল।");

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                schema: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "রেকর্ডটি কোন পেশেন্টের, তার আইডি।"),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "কোন ডাক্তার এই চেকআপটি করেছেন বা রেকর্ডটি তৈরি করেছেন।"),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "ডাক্তারের সাথে ভিজিট বা চেকআপের তারিখ ও সময়।"),
                    ChiefComplaint = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "পেশেন্টের প্রধান সমস্যা বা অভিযোগের বর্ণনা।"),
                    Diagnosis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "ডাক্তার কর্তৃক নির্ণীত রোগ বা স্বাস্থ্য সমস্যার নাম।"),
                    DifferentialDiagnosis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "সম্ভাব্য অন্যান্য রোগ যেগুলোর লক্ষণ বর্তমান রোগের সাথে মিলে।"),
                    TreatmentPlan = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true, comment: "চিকিৎসার পরিকল্পনা বা পরবর্তী পদক্ষেপের বিস্তারিত।"),
                    Prescription = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true, comment: "পেশেন্টকে দেওয়া ওষুধের তালিকা বা প্রেসক্রিপশন সামারি।"),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true, comment: "ডাক্তারের ব্যক্তিগত ক্লিনিকাল পর্যবেক্ষণ বা অতিরিক্ত নোট।"),
                    ReferralTo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "যদি পেশেন্টকে অন্য কোনো বিশেষজ্ঞ বা হসপিটালে রেফার করা হয়।"),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "পরবর্তী ফলো-আপ বা চেকআপের সম্ভাব্য তারিখ।"),
                    AttachmentUrls = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "রিপোর্ট, এক্স-রে বা স্ক্যানের লিংকের তালিকা (JSON ফরম্যাটে)।"),
                    IsSharedWithPatient = table.Column<bool>(type: "bit", nullable: false, comment: "এই রেকর্ডটি পেশেন্ট তার নিজের অ্যাপ থেকে দেখতে পারবে কি না।"),
                    DoctorEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Doctors_DoctorEntityId",
                        column: x => x.DoctorEntityId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "পেশেন্টদের ক্লিনিকাল ভিজিট, রোগ নির্ণয় এবং চিকিৎসার বিস্তারিত রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "PatientReferrals",
                schema: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferralCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReferringDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferredToDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReferredToDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredToHospital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ClinicalSummary = table.Column<string>(type: "nvarchar(max)", maxLength: 3000, nullable: true),
                    UrgencyLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReferrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientReferrals_Doctors_ReferredToDoctorId",
                        column: x => x.ReferredToDoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientReferrals_Doctors_ReferringDoctorId",
                        column: x => x.ReferringDoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientReferrals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পেশেন্টদের এক ডাক্তার বা ডিপার্টমেন্ট থেকে অন্য জায়গায় রেফার করার রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescribedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsRefillable = table.Column<bool>(type: "bit", nullable: false),
                    RefillCount = table.Column<int>(type: "int", nullable: false),
                    IsSharedWithPatient = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ডাক্তার কর্তৃক পেশেন্টকে দেওয়া ওষুধের প্রেসক্রিপশন রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "StaffAttendances",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckInTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    CheckOutTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    IsOnLeave = table.Column<bool>(type: "bit", nullable: false),
                    LeaveReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "ছুটি নিয়ে থাকলে তার কারণ।"),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "উপস্থিতি সংক্রান্ত অতিরিক্ত নোট।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAttendances_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "Finance",
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "হাসপাতালের স্টাফদের প্রতিদিনের উপস্থিতি এবং ছুটির রেকর্ড টেবিল।");

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "অটো-জেনারেটেড ইউনিক বিল নাম্বার।"),
                    BillType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "Finance",
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Finance_Bills_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "রোগীর বিভিন্ন সেবার বিপরীতে তৈরি করা মূল বিল বা ইনভয়েস মাস্টার টেবিল।");

            migrationBuilder.CreateTable(
                name: "AmbulanceDashboardMetrics",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetricDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalBookingsToday = table.Column<int>(type: "int", nullable: false),
                    CompletedBookings = table.Column<int>(type: "int", nullable: false),
                    PendingBookings = table.Column<int>(type: "int", nullable: false),
                    CancelledBookings = table.Column<int>(type: "int", nullable: false),
                    TotalVehicles = table.Column<int>(type: "int", nullable: false),
                    AvailableVehicles = table.Column<int>(type: "int", nullable: false),
                    VehiclesOnDuty = table.Column<int>(type: "int", nullable: false),
                    VehiclesUnderMaintenance = table.Column<int>(type: "int", nullable: false),
                    TodayEarnings = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyEarnings = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PendingPayments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceDashboardMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceDashboardMetrics_AmbulanceProviderProfiles_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceProviderProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "অ্যাম্বুলেন্স প্রোভাইডারদের ব্যক্তিগত ড্যাশবোর্ড পরিসংখ্যানের টেবিল।");

            migrationBuilder.CreateTable(
                name: "AmbulanceProviderWallets",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "কোন প্রোভাইডারের এই ওয়ালেট তার রেফারেন্স।"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m, comment: "প্রোভাইডারের বর্তমান এভেইলেবল ব্যালেন্স।"),
                    TotalEarned = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m, comment: "প্রোভাইডারের এ পর্যন্ত মোট উপার্জিত অর্থ।"),
                    TotalWithdrawn = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m, comment: "প্রোভাইডারের এ পর্যন্ত মোট উইথড্র করা অর্থ।"),
                    LastTransactionAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceProviderWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceProviderWallets_AmbulanceProviderProfiles_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceProviderProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "অ্যাম্বুলেন্স প্রোভাইডারদের আয় এবং ব্যালেন্স ট্র্যাকিং ওয়ালেট টেবিল।");

            migrationBuilder.CreateTable(
                name: "AmbulanceServiceListings",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "এই সার্ভিসটি কোন প্রোভাইডারের তার রেফারেন্স (Foreign Key)।"),
                    ServiceTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "সার্ভিসের নাম বা শিরোনাম।"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "সার্ভিসের বিস্তারিত বর্ণনা।"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "সার্ভিসের ছবি বা আইকনের ইউআরএল।"),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "সার্ভিসের ভিত্তি মূল্য বা শুরুর ভাড়া।"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    FeedStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceServiceListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceServiceListings_AmbulanceProviderProfiles_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceProviderProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "প্রোভাইডারদের অফার করা বিভিন্ন অ্যাম্বুলেন্স সার্ভিসের তালিকা (যেমন: AC, ICU, ফ্রিজার ভ্যান)।");

            migrationBuilder.CreateTable(
                name: "AmbulanceVehicles",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "এই গাড়িটি কোন প্রোভাইডারের তার রেফারেন্স।"),
                    VehicleNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "গাড়ির লাইসেন্স প্লেট বা রেজিস্ট্রেশন নম্বর।"),
                    AmbulanceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "অভ্যন্তরীণ চেনার জন্য বিশেষ কোড (যেমন: AMB-001)।"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HasVentilator = table.Column<bool>(type: "bit", nullable: false),
                    HasDefibrillator = table.Column<bool>(type: "bit", nullable: false),
                    HasOxygen = table.Column<bool>(type: "bit", nullable: false),
                    HasStretcherWheelchair = table.Column<bool>(type: "bit", nullable: false),
                    CurrentLatitude = table.Column<double>(type: "float", nullable: true),
                    CurrentLongitude = table.Column<double>(type: "float", nullable: true),
                    LastLocationUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "গাড়িচালকের নাম।"),
                    DriverPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "গাড়িচালকের মোবাইল নম্বর।"),
                    ParamedicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "গাড়িতে উপস্থিত প্যারামেডিক বা সহকারীর নাম (ঐচ্ছিক)।"),
                    ParamedicPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextMaintenanceDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "গাড়ি সংক্রান্ত বিশেষ কোনো নোট।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceVehicles_AmbulanceProviderProfiles_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceProviderProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "অ্যাম্বুলেন্স প্রোভাইডারদের অধীনে থাকা গাড়িসমূহের তথ্য।");

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderType = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AttachmentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AttachmentSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalSchema: "Social",
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "সিস্টেমের সব ইউজারদের মধ্যকার চ্যাট মেসেজ স্টোর করার টেবিল।");

            migrationBuilder.CreateTable(
                name: "FeedItemLikes",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItemLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItemLikes_FeedItems_FeedItemId",
                        column: x => x.FeedItemId,
                        principalSchema: "Social",
                        principalTable: "FeedItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedItemLikes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                },
                comment: "ফিড আইটেমগুলোতে ইউজারদের দেওয়া লাইক বা রিঅ্যাকশন।");

            migrationBuilder.CreateTable(
                name: "FeedItemSaves",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItemSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItemSaves_FeedItems_FeedItemId",
                        column: x => x.FeedItemId,
                        principalSchema: "Social",
                        principalTable: "FeedItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedItemSaves_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                },
                comment: "ইউজারদের সেভ করা ফিড আইটেমগুলোর রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "FeedItemTags",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "ট্যাগের নাম বা টেক্সট।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedItemTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedItemTags_FeedItems_FeedItemId",
                        column: x => x.FeedItemId,
                        principalSchema: "Social",
                        principalTable: "FeedItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ফিড আইটেমগুলোতে ব্যবহৃত বিভিন্ন ট্যাগ (যেমন: Health, Medical) এর রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkedFeedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FeedItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_FeedItems_FeedItemId",
                        column: x => x.FeedItemId,
                        principalSchema: "Social",
                        principalTable: "FeedItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_FeedItems_LinkedFeedItemId",
                        column: x => x.LinkedFeedItemId,
                        principalSchema: "Social",
                        principalTable: "FeedItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorUserId",
                        column: x => x.AuthorUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ইউজারদের তৈরি করা বিভিন্ন ফিড পোস্ট (যেমন: হেলথ টিপস, জেনারেল পোস্ট) এর মূল তথ্য।");

            migrationBuilder.CreateTable(
                name: "LabDashboardMetrics",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetricDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalOrdersToday = table.Column<int>(type: "int", nullable: false),
                    CompletedOrders = table.Column<int>(type: "int", nullable: false),
                    PendingOrders = table.Column<int>(type: "int", nullable: false),
                    SampleCollectedOrders = table.Column<int>(type: "int", nullable: false),
                    ReportDeliveredOrders = table.Column<int>(type: "int", nullable: false),
                    TotalTestsOffered = table.Column<int>(type: "int", nullable: false),
                    ActiveTests = table.Column<int>(type: "int", nullable: false),
                    TodayRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PendingPayments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabDashboardMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabDashboardMetrics_LabProfiles_LabProfileId",
                        column: x => x.LabProfileId,
                        principalSchema: "Patient",
                        principalTable: "LabProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ল্যাব প্রোফাইলগুলোর দৈনিক ড্যাশবোর্ড পরিসংখ্যান এবং আয় ট্র্যাকিং টেবিল।");

            migrationBuilder.CreateTable(
                name: "LabOrders",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderedByDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LabProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsFasting = table.Column<bool>(type: "bit", nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsSharedWithPatient = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabOrders_Doctors_OrderedByDoctorId",
                        column: x => x.OrderedByDoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LabOrders_LabProfiles_LabProfileId",
                        column: x => x.LabProfileId,
                        principalSchema: "Patient",
                        principalTable: "LabProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabOrders_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ল্যাব টেস্টের অর্ডার এবং ক্লিনিকাল তথ্য।");

            migrationBuilder.CreateTable(
                name: "LabServiceListings",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HomeCollectionAvailable = table.Column<bool>(type: "bit", nullable: false),
                    HomeCollectionCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FeedStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabServiceListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabServiceListings_LabProfiles_LabProfileId",
                        column: x => x.LabProfileId,
                        principalSchema: "Patient",
                        principalTable: "LabProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ল্যাবরেটরির অফার করা বিভিন্ন সার্ভিস ও ফিড লিস্টিং।");

            migrationBuilder.CreateTable(
                name: "LabTests",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TestCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SampleType = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceRange = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Preparation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TurnAroundTimeHours = table.Column<int>(type: "int", nullable: false),
                    EquipmentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTests_LabProfiles_LabProfileId",
                        column: x => x.LabProfileId,
                        principalSchema: "Patient",
                        principalTable: "LabProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ল্যাবরেটরির অফার করা সুনির্দিষ্ট টেস্টগুলোর মাস্টার লিস্ট।");

            migrationBuilder.CreateTable(
                name: "Medicines",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacyProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GenericName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MedicineCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Strength = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RequiresPrescription = table.Column<bool>(type: "bit", nullable: false),
                    SideEffects = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Contraindications = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StorageConditions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_PharmacyProfiles_PharmacyProfileId",
                        column: x => x.PharmacyProfileId,
                        principalSchema: "Inventory",
                        principalTable: "PharmacyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "হাসপাতাল ফার্মেসির ওষুধের তালিকা।");

            migrationBuilder.CreateTable(
                name: "PharmacyDashboardMetrics",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacyProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetricDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalOrdersToday = table.Column<int>(type: "int", nullable: false),
                    CompletedOrders = table.Column<int>(type: "int", nullable: false),
                    PendingOrders = table.Column<int>(type: "int", nullable: false),
                    OutForDeliveryOrders = table.Column<int>(type: "int", nullable: false),
                    CancelledOrders = table.Column<int>(type: "int", nullable: false),
                    TotalMedicines = table.Column<int>(type: "int", nullable: false),
                    LowStockMedicines = table.Column<int>(type: "int", nullable: false),
                    OutOfStockMedicines = table.Column<int>(type: "int", nullable: false),
                    ExpiringThisMonth = table.Column<int>(type: "int", nullable: false),
                    TodayRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PendingPayments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyDashboardMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacyDashboardMetrics_PharmacyProfiles_PharmacyProfileId",
                        column: x => x.PharmacyProfileId,
                        principalSchema: "Inventory",
                        principalTable: "PharmacyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ফার্মেসিগুলোর দৈনিক অর্ডার, ইনভেন্টরি স্ট্যাটাস এবং আয়ের পরিসংখ্যান টেবিল।");

            migrationBuilder.CreateTable(
                name: "PharmacyServiceListings",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PharmacyProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsDeliveryAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FeedStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyServiceListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacyServiceListings_PharmacyProfiles_PharmacyProfileId",
                        column: x => x.PharmacyProfileId,
                        principalSchema: "Inventory",
                        principalTable: "PharmacyProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ফার্মেসির অফার বা সার্ভিস যা ফিড-এ প্রদর্শিত হবে।");

            migrationBuilder.CreateTable(
                name: "Admissions",
                schema: "Ward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmittingDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DischargeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AdmissionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DischargeNotes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DischargeSummary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsTransferred = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TransferredFrom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TransferredTo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TransferReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admissions_Beds_BedId",
                        column: x => x.BedId,
                        principalSchema: "Ward",
                        principalTable: "Beds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admissions_Doctors_AdmittingDoctorId",
                        column: x => x.AdmittingDoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admissions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "হাসপাতালের ইন-পেশেন্ট বা ওয়ার্ডে ভর্তি হওয়া রোগীদের বিস্তারিত তথ্য।");

            migrationBuilder.CreateTable(
                name: "BedBookings",
                schema: "Ward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    TotalCharge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BookingStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SpecialRequests = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BedBookings_Beds_BedId",
                        column: x => x.BedId,
                        principalSchema: "Ward",
                        principalTable: "Beds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BedBookings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BedBookings_Users_BookedByUserId",
                        column: x => x.BookedByUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ইউজারদের সরাসরি অ্যাপ থেকে বেড বুকিং এবং পেমেন্ট সংক্রান্ত তথ্য।");

            migrationBuilder.CreateTable(
                name: "DoctorScheduleSlots",
                schema: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlotDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlotStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SlotEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    BlockReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "স্লটটি ব্লক করা হলে তার কারণ (উদা: জরুরি মিটিং)।"),
                    IsTelemedicine = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorScheduleSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorScheduleSlots_DoctorSchedules_DoctorScheduleId",
                        column: x => x.DoctorScheduleId,
                        principalSchema: "Staff",
                        principalTable: "DoctorSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorScheduleSlots_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ডাক্তারদের শিডিউলের অধীনে প্রতিটি নির্দিষ্ট সময়ের স্লট।");

            migrationBuilder.CreateTable(
                name: "MedicineOrders",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineOrders_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicineOrders_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalSchema: "Inventory",
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "ফার্মেসি থেকে ওষুধের অর্ডারের মূল রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    AppointmentType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReasonForVisit = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Symptoms = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsFirstVisit = table.Column<bool>(type: "bit", nullable: false),
                    IsFollowUp = table.Column<bool>(type: "bit", nullable: false),
                    PreviousAppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReminderSent = table.Column<bool>(type: "bit", nullable: false),
                    ReminderSentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsManualBooking = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    CancellationReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Appointments_PreviousAppointmentId",
                        column: x => x.PreviousAppointmentId,
                        principalSchema: "Patient",
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Finance",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "রোগী এবং ডাক্তারদের মধ্যকার অ্যাপয়েন্টমেন্ট বা সিরিয়াল ম্যানেজমেন্ট টেবিল।");

            migrationBuilder.CreateTable(
                name: "BillItems",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Finance",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "বিলের অন্তর্ভুক্ত প্রতিটি সার্ভিস বা আইটেমের বিস্তারিত তালিকা (যেমন: ডক্টর ফি, টেস্ট ফি)।");

            migrationBuilder.CreateTable(
                name: "InsuranceClaims",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClaimNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsuranceProvider = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PolicyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PolicyHolderName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    RejectedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocumentUrls = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Finance",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "বিলের বিপরীতে পেশেন্টের ইন্স্যুরেন্স ক্লেইম এবং স্ট্যাটাস ট্র্যাকিং টেবিল।");

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gateway = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GatewayTransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GatewayResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsRefunded = table.Column<bool>(type: "bit", nullable: false),
                    RefundedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefundTransactionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReceivedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Finance",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "সিস্টেমের সকল পেমেন্ট এবং লেনদেনের বিস্তারিত তথ্য।");

            migrationBuilder.CreateTable(
                name: "AmbulanceBookings",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "বুকিং রেফারেন্স নম্বর যা ইউনিক হিসেবে ব্যবহৃত হয়।"),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequesterName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "যে ব্যক্তি অ্যাম্বুলেন্সটি কল করেছেন বা অর্ডার করেছেন।"),
                    RequesterPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "রিকোয়েস্ট কারীর মোবাইল নম্বর।"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispatchedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PickupAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "যেখান থেকে রোগীকে তোলা হবে।"),
                    PickupLatitude = table.Column<double>(type: "float", nullable: true),
                    PickupLongitude = table.Column<double>(type: "float", nullable: true),
                    DropAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "গন্তব্য বা হাসপাতালের ঠিকানা।"),
                    DropLatitude = table.Column<double>(type: "float", nullable: true),
                    DropLongitude = table.Column<double>(type: "float", nullable: true),
                    EmergencyDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "জরুরি অবস্থার সংক্ষিপ্ত বর্ণনা।"),
                    CancellationReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "বুকিং বাতিল হলে তার কারণ।"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fare = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true, comment: "অ্যাম্বুলেন্স সার্ভিস চার্জ বা ভাড়া।"),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDestination = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceBookings_AmbulanceProviderProfiles_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceProviderProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AmbulanceBookings_AmbulanceVehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AmbulanceBookings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "অ্যাম্বুলেন্স বুকিং এবং এর বর্তমান অবস্থা ট্র্যাকিং টেবিল।");

            migrationBuilder.CreateTable(
                name: "PostAudiences",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Visibility = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "পোস্টের ভিজিবিলিটি লেভেল (যেমন: Public, Private, Anonymous)।"),
                    TargetCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "যদি নির্দিষ্ট কোনো শহরের অডিয়েন্সকে টার্গেট করা হয়।"),
                    TargetSpecialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "যদি নির্দিষ্ট কোনো মেডিকেল স্পেশালিটির ইউজারদের জন্য পোস্টটি করা হয়।"),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostAudiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostAudiences_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Social",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "একটি নির্দিষ্ট পোস্টের অডিয়েন্স সেটিংস এবং ভিজিবিলিটি (কে কে দেখতে পারবে) সংক্রান্ত তথ্য।");

            migrationBuilder.CreateTable(
                name: "PostContents",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextBody = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true, comment: "পোস্টের সাধারণ টেক্সট কন্টেন্ট।"),
                    PostType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "পোস্টের ধরন বা ক্যাটাগরি।"),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "পোস্টের ভাষা।"),
                    FormattedContent = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "ফরম্যাটেড কন্টেন্ট বা রিচ টেক্সট সংরক্ষণের জন্য।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostContents_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Social",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পোস্টের মূল বিষয়বস্তু বা কন্টেন্ট (টেক্সট, ফরম্যাটেড টেক্সট এবং ল্যাঙ্গুয়েজ) এর তথ্য।");

            migrationBuilder.CreateTable(
                name: "PostInteractions",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostInteractions_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Social",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পোস্টের যাবতীয় ইন্টারেকশন (লাইক, কমেন্ট, শেয়ার, সেভ) এর সামারি এবং কালেকশন হাব।");

            migrationBuilder.CreateTable(
                name: "PostMedias",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "মিডিয়া ফাইলের (Image/Video) ডাইরেক্ট স্টোরেজ লিঙ্ক।"),
                    MediaType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "ভিডিওর ক্ষেত্রে থাম্বনেইল ছবির লিঙ্ক।"),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "একাধিক মিডিয়া থাকলে সেগুলোর প্রদর্শনের ক্রম।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMedias_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Social",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পোস্টের সাথে যুক্ত ছবি বা ভিডিওর তথ্য।");

            migrationBuilder.CreateTable(
                name: "PostMetas",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Draft"),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "পোস্টটি ফিডের ওপরে পিন করা থাকবে কি না।"),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "পোস্টটি ঠিক কখন পাবলিশ করা হয়েছে।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMetas_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Social",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পোস্টের মেটা ডাটা যেমন স্ট্যাটাস, পিনিং এবং পাবলিশ টাইম।");

            migrationBuilder.CreateTable(
                name: "LabOrderItems",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabTestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SampleCollectedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleCollectedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SampleBarcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResultValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ResultUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceRange = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAbnormal = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ResultEnteredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultEnteredBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAutoUploaded = table.Column<bool>(type: "bit", nullable: false),
                    ReportDeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabOrderItems_LabOrders_LabOrderId",
                        column: x => x.LabOrderId,
                        principalSchema: "Patient",
                        principalTable: "LabOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabOrderItems_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalSchema: "Patient",
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "ল্যাব অর্ডারের অন্তর্গত প্রতিটি সুনির্দিষ্ট টেস্টের ফলাফল ও স্ট্যাটাস।");

            migrationBuilder.CreateTable(
                name: "MedicineStocks",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    StorageLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false),
                    ReorderQuantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StockStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineStocks_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalSchema: "Inventory",
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineStocks_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Inventory",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "ওষুধের ব্যাচভিত্তিক ইনভেন্টরি এবং সাপ্লাই রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "PrescriptionItems",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Route = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WithFood = table.Column<bool>(type: "bit", nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDispensed = table.Column<bool>(type: "bit", nullable: false),
                    DispensedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionItems_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalSchema: "Inventory",
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionItems_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalSchema: "Inventory",
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "প্রেসক্রিপশনে থাকা নির্দিষ্ট ওষুধের ডোজ এবং নিয়মাবলী।");

            migrationBuilder.CreateTable(
                name: "MedicineOrderItems",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineOrderItems_MedicineOrders_MedicineOrderId",
                        column: x => x.MedicineOrderId,
                        principalSchema: "Inventory",
                        principalTable: "MedicineOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineOrderItems_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalSchema: "Inventory",
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "প্রতিটি মেডিসিন অর্ডারের অন্তর্ভুক্ত ওষুধের বিস্তারিত তালিকা।");

            migrationBuilder.CreateTable(
                name: "AppointmentReminders",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Channel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "নোটিফিকেশন ফেইল করলে তার কারণ বা এরর মেসেজ।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentReminders_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "Patient",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "রোগীদের অ্যাপয়েন্টমেন্টের নোটিফিকেশন বা রিমাইন্ডার পাঠানোর লগ টেবিল।");

            migrationBuilder.CreateTable(
                name: "TelemedicineSessions",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RoomId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MeetingLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatientToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationMinutes = table.Column<int>(type: "int", nullable: true),
                    DoctorNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Prescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    FollowUpInstructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRecorded = table.Column<bool>(type: "bit", nullable: false),
                    RecordingUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemedicineSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelemedicineSessions_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "Patient",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelemedicineSessions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelemedicineSessions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "টেলিকনসালটেশন বা অনলাইন ভিডিও কলের সেশন রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "PlatformWalletTransactions",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TransactionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformWalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformWalletTransactions_PaymentTransactions_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Finance",
                        principalTable: "PaymentTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PlatformWalletTransactions_PlatformWallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "Finance",
                        principalTable: "PlatformWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "প্ল্যাটফর্ম ওয়ালেটের প্রতিটি ডেবিট/ক্রেডিট ট্রানজ্যাকশন হিস্ট্রি।");

            migrationBuilder.CreateTable(
                name: "AmbulanceWalletTransactions",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "কোন ওয়ালেটের আন্ডারে এই ট্রানজেকশনটি হয়েছে।"),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "ইউনিক ট্রানজেকশন আইডি বা রেফারেন্স কোড।"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "লেনদেনের পরিমাণ (টাকা)।"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "লেনদেনের সংক্ষিপ্ত বিবরণ।"),
                    TransactionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "এই লেনদেনটি হওয়ার পর ওয়ালেটের মোট ব্যালেন্স।"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulanceWalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmbulanceWalletTransactions_AmbulanceBookings_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AmbulanceWalletTransactions_AmbulanceProviderWallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceProviderWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "অ্যাম্বুলেন্স প্রোভাইডারদের ওয়ালেটের প্রতিটি লেনদেনের (আয়/উইথড্র) বিস্তারিত রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "EmergencyVisits",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmergencyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "প্রতিটি ইমারজেন্সি ভিজিটের জন্য ইউনিক ট্র্যাকিং কোড।"),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "সংশ্লিষ্ট রোগীর আইডি।"),
                    AttendingDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "দায়িত্বপ্রাপ্ত চিকিৎসকের আইডি।"),
                    AmbulanceBookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "অ্যাম্বুলেন্স বুকিং রেফারেন্স।"),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TriageTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DischargeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TriageLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "রোগীর গুরুত্বের স্তর।"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "ইমারজেন্সি ভিজিটের বর্তমান অবস্থা।"),
                    ChiefComplaint = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "রোগীর প্রধান সমস্যা বা অভিযোগের বর্ণনা।"),
                    ModeOfArrival = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "রোগী কীভাবে হাসপাতালে এসেছেন (যেমন: Ambulance, Private Car, Walk-in)।"),
                    IsAmbulance = table.Column<bool>(type: "bit", nullable: false),
                    InitialAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DischargeNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmitted = table.Column<bool>(type: "bit", nullable: false),
                    AdmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsTransferred = table.Column<bool>(type: "bit", nullable: false),
                    TransferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeceased = table.Column<bool>(type: "bit", nullable: false),
                    DeathCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyVisits_AmbulanceBookings_AmbulanceBookingId",
                        column: x => x.AmbulanceBookingId,
                        principalSchema: "Inventory",
                        principalTable: "AmbulanceBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EmergencyVisits_Doctors_AttendingDoctorId",
                        column: x => x.AttendingDoctorId,
                        principalSchema: "Staff",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EmergencyVisits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Patients",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "হাসপাতালের ইমারজেন্সি বা জরুরি বিভাগে আসা রোগীদের ভিজিট রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "PostComments",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostInteractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "ইউজারের করা কমেন্টের টেক্সট কন্টেন্ট।"),
                    ParentCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_PostComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalSchema: "Social",
                        principalTable: "PostComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostComments_PostInteractions_PostInteractionId",
                        column: x => x.PostInteractionId,
                        principalSchema: "Social",
                        principalTable: "PostInteractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "পোস্টের বিপরীতে ইউজারদের করা কমেন্ট এবং রিপ্লাইগুলো এখানে সংরক্ষিত থাকে।");

            migrationBuilder.CreateTable(
                name: "PostLikes",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostInteractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLikes_PostInteractions_PostInteractionId",
                        column: x => x.PostInteractionId,
                        principalSchema: "Social",
                        principalTable: "PostInteractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLikes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "পোস্টের বিপরীতে ইউজারদের দেওয়া লাইক বা রিঅ্যাকশন।");

            migrationBuilder.CreateTable(
                name: "PostSaves",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostInteractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostSaves_PostInteractions_PostInteractionId",
                        column: x => x.PostInteractionId,
                        principalSchema: "Social",
                        principalTable: "PostInteractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostSaves_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারদের সেভ করে রাখা পোস্টগুলোর রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "PostShares",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostInteractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "শেয়ার করার সময় ইউজারের দেওয়া ঐচ্ছিক নোট।"),
                    SharedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostShares_PostInteractions_PostInteractionId",
                        column: x => x.PostInteractionId,
                        principalSchema: "Social",
                        principalTable: "PostInteractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostShares_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ইউজারদের করা পোস্ট শেয়ারের রেকর্ড।");

            migrationBuilder.CreateTable(
                name: "PostTags",
                schema: "Social",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "ট্যাগের নাম (যেমন: HealthTips, Cardiology)।"),
                    PostMetaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTags_PostMetas_PostMetaId",
                        column: x => x.PostMetaId,
                        principalSchema: "Social",
                        principalTable: "PostMetas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Social",
                        principalTable: "Posts",
                        principalColumn: "Id");
                },
                comment: "পোস্টের সাথে যুক্ত বিভিন্ন ট্যাগ বা কি-ওয়ার্ড।");

            migrationBuilder.CreateIndex(
                name: "IX_AdminDashboardMetrics_MetricDate",
                schema: "Finance",
                table: "AdminDashboardMetrics",
                column: "MetricDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_AdmissionCode",
                schema: "Ward",
                table: "Admissions",
                column: "AdmissionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_AdmittingDoctorId",
                schema: "Ward",
                table: "Admissions",
                column: "AdmittingDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_BedId",
                schema: "Ward",
                table: "Admissions",
                column: "BedId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_PatientId",
                schema: "Ward",
                table: "Admissions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_Status",
                schema: "Ward",
                table: "Admissions",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceBookings_BookingCode",
                schema: "Inventory",
                table: "AmbulanceBookings",
                column: "BookingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceBookings_CreatedAt",
                schema: "Inventory",
                table: "AmbulanceBookings",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceBookings_PatientId",
                schema: "Inventory",
                table: "AmbulanceBookings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceBookings_ProviderId",
                schema: "Inventory",
                table: "AmbulanceBookings",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceBookings_RequesterPhone",
                schema: "Inventory",
                table: "AmbulanceBookings",
                column: "RequesterPhone");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceBookings_VehicleId",
                schema: "Inventory",
                table: "AmbulanceBookings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceDashboardMetrics_ProviderId_MetricDate",
                schema: "Finance",
                table: "AmbulanceDashboardMetrics",
                columns: new[] { "ProviderId", "MetricDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceProviderProfiles_ApplicationUserId",
                schema: "Inventory",
                table: "AmbulanceProviderProfiles",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceProviderProfiles_EmergencyHotline",
                schema: "Inventory",
                table: "AmbulanceProviderProfiles",
                column: "EmergencyHotline");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceProviderProfiles_RegistrationNumber",
                schema: "Inventory",
                table: "AmbulanceProviderProfiles",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceProviderWallets_ProviderId",
                schema: "Inventory",
                table: "AmbulanceProviderWallets",
                column: "ProviderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceServiceListings_ProviderId",
                schema: "Inventory",
                table: "AmbulanceServiceListings",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceServiceListings_ServiceTitle",
                schema: "Inventory",
                table: "AmbulanceServiceListings",
                column: "ServiceTitle");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceVehicles_AmbulanceCode",
                schema: "Inventory",
                table: "AmbulanceVehicles",
                column: "AmbulanceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceVehicles_ProviderId",
                schema: "Inventory",
                table: "AmbulanceVehicles",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceVehicles_VehicleNumber",
                schema: "Inventory",
                table: "AmbulanceVehicles",
                column: "VehicleNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceWalletTransactions_BookingId",
                schema: "Inventory",
                table: "AmbulanceWalletTransactions",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceWalletTransactions_TransactionCode",
                schema: "Inventory",
                table: "AmbulanceWalletTransactions",
                column: "TransactionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulanceWalletTransactions_WalletId",
                schema: "Inventory",
                table: "AmbulanceWalletTransactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentReminders_AppointmentId",
                schema: "Patient",
                table: "AppointmentReminders",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentReminders_Status_ScheduledAt",
                schema: "Patient",
                table: "AppointmentReminders",
                columns: new[] { "Status", "ScheduledAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentCode",
                schema: "Patient",
                table: "Appointments",
                column: "AppointmentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BillId",
                schema: "Patient",
                table: "Appointments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate",
                schema: "Patient",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId_AppointmentDate",
                schema: "Patient",
                table: "Appointments",
                columns: new[] { "PatientId", "AppointmentDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PreviousAppointmentId",
                schema: "Patient",
                table: "Appointments",
                column: "PreviousAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Status",
                schema: "Patient",
                table: "Appointments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName",
                schema: "Identity",
                table: "AuditLogs",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Timestamp",
                schema: "Identity",
                table: "AuditLogs",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                schema: "Identity",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BedBookings_BedId",
                schema: "Ward",
                table: "BedBookings",
                column: "BedId");

            migrationBuilder.CreateIndex(
                name: "IX_BedBookings_BookedByUserId",
                schema: "Ward",
                table: "BedBookings",
                column: "BookedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BedBookings_BookingCode",
                schema: "Ward",
                table: "BedBookings",
                column: "BookingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BedBookings_CheckInDate",
                schema: "Ward",
                table: "BedBookings",
                column: "CheckInDate");

            migrationBuilder.CreateIndex(
                name: "IX_BedBookings_IsPaid",
                schema: "Ward",
                table: "BedBookings",
                column: "IsPaid");

            migrationBuilder.CreateIndex(
                name: "IX_BedBookings_PatientId",
                schema: "Ward",
                table: "BedBookings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Beds_WardId_BedNumber",
                schema: "Ward",
                table: "Beds",
                columns: new[] { "WardId", "BedNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                schema: "Finance",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillDate",
                schema: "Finance",
                table: "Bills",
                column: "BillDate");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillNumber",
                schema: "Finance",
                table: "Bills",
                column: "BillNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_InvoiceId",
                schema: "Finance",
                table: "Bills",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientId",
                schema: "Finance",
                table: "Bills",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PaymentStatus",
                schema: "Finance",
                table: "Bills",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId",
                schema: "Social",
                table: "ChatMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                schema: "Social",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ParticipantAId",
                schema: "Social",
                table: "Conversations",
                column: "ParticipantAId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ParticipantBId",
                schema: "Social",
                table: "Conversations",
                column: "ParticipantBId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ReferenceId",
                schema: "Social",
                table: "Conversations",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardMetrics_MetricDate",
                schema: "Finance",
                table: "DashboardMetrics",
                column: "MetricDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Code",
                schema: "Staff",
                table: "Departments",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                schema: "Staff",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilityLogs_ChangedAt",
                schema: "Staff",
                table: "DoctorAvailabilityLogs",
                column: "ChangedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilityLogs_DoctorEntityId",
                schema: "Staff",
                table: "DoctorAvailabilityLogs",
                column: "DoctorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAvailabilityLogs_DoctorId",
                schema: "Staff",
                table: "DoctorAvailabilityLogs",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDashboardMetrics_DoctorId_MetricDate",
                schema: "Finance",
                table: "DoctorDashboardMetrics",
                columns: new[] { "DoctorId", "MetricDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDocuments_DoctorId",
                schema: "Staff",
                table: "DoctorDocuments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorEarnings_DoctorId",
                schema: "Staff",
                table: "DoctorEarnings",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorEarnings_EarningDate",
                schema: "Staff",
                table: "DoctorEarnings",
                column: "EarningDate");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorFeedbackSummaries_DoctorId",
                schema: "Staff",
                table: "DoctorFeedbackSummaries",
                column: "DoctorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorLeaves_DoctorId",
                schema: "Staff",
                table: "DoctorLeaves",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorLeaves_LeaveFrom_LeaveTo",
                schema: "Staff",
                table: "DoctorLeaves",
                columns: new[] { "LeaveFrom", "LeaveTo" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNotes_DoctorId",
                schema: "Staff",
                table: "DoctorNotes",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNotes_PatientId",
                schema: "Staff",
                table: "DoctorNotes",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNotifications_DeliveredAt",
                schema: "Staff",
                table: "DoctorNotifications",
                column: "DeliveredAt");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorNotifications_DoctorId_IsRead",
                schema: "Staff",
                table: "DoctorNotifications",
                columns: new[] { "DoctorId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPerformanceReports_DoctorId",
                schema: "Staff",
                table: "DoctorPerformanceReports",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPerformanceReports_FromDate_ToDate",
                schema: "Staff",
                table: "DoctorPerformanceReports",
                columns: new[] { "FromDate", "ToDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ApplicationUserId",
                schema: "Staff",
                table: "Doctors",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                schema: "Staff",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorCode",
                schema: "Staff",
                table: "Doctors",
                column: "DoctorCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_LicenseNumber",
                schema: "Staff",
                table: "Doctors",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId_DayOfWeek_IsAvailable",
                schema: "Staff",
                table: "DoctorSchedules",
                columns: new[] { "DoctorId", "DayOfWeek", "IsAvailable" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId_DayOfWeek_ShiftType",
                schema: "Staff",
                table: "DoctorSchedules",
                columns: new[] { "DoctorId", "DayOfWeek", "ShiftType" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleSlots_DoctorId_SlotDate_SlotStartTime",
                schema: "Staff",
                table: "DoctorScheduleSlots",
                columns: new[] { "DoctorId", "SlotDate", "SlotStartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleSlots_DoctorScheduleId",
                schema: "Staff",
                table: "DoctorScheduleSlots",
                column: "DoctorScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSlot_Search",
                schema: "Staff",
                table: "DoctorScheduleSlots",
                columns: new[] { "DoctorId", "SlotDate", "IsBooked", "IsBlocked" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorUnavailabilities_DoctorId_UnavailableDate",
                schema: "Finance",
                table: "DoctorUnavailabilities",
                columns: new[] { "DoctorId", "UnavailableDate" });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyVisits_AmbulanceBookingId",
                schema: "Patient",
                table: "EmergencyVisits",
                column: "AmbulanceBookingId",
                unique: true,
                filter: "[AmbulanceBookingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyVisits_AttendingDoctorId",
                schema: "Patient",
                table: "EmergencyVisits",
                column: "AttendingDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyVisits_EmergencyCode",
                schema: "Patient",
                table: "EmergencyVisits",
                column: "EmergencyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyVisits_PatientId_CreatedAt",
                schema: "Patient",
                table: "EmergencyVisits",
                columns: new[] { "PatientId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyVisits_Status",
                schema: "Patient",
                table: "EmergencyVisits",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyVisits_TriageLevel",
                schema: "Patient",
                table: "EmergencyVisits",
                column: "TriageLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PatientId",
                schema: "Social",
                table: "Feedbacks",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItem_User_Unique_Like",
                schema: "Social",
                table: "FeedItemLikes",
                columns: new[] { "FeedItemId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedItemLikes_UserId",
                schema: "Social",
                table: "FeedItemLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItems_OwnerUserId",
                schema: "Social",
                table: "FeedItems",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItem_User_Unique_Save",
                schema: "Social",
                table: "FeedItemSaves",
                columns: new[] { "FeedItemId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedItemSaves_UserId",
                schema: "Social",
                table: "FeedItemSaves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItem_Tag_Unique",
                schema: "Social",
                table: "FeedItemTags",
                columns: new[] { "FeedItemId", "Tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogs_LoggedAt",
                schema: "Patients",
                table: "HealthLogs",
                column: "LoggedAt");

            migrationBuilder.CreateIndex(
                name: "IX_HealthLogs_PatientId",
                schema: "Patients",
                table: "HealthLogs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_BillId",
                schema: "Finance",
                table: "InsuranceClaims",
                column: "BillId",
                unique: true,
                filter: "[BillId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_ClaimNumber",
                schema: "Finance",
                table: "InsuranceClaims",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                schema: "Finance",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PatientId",
                schema: "Finance",
                table: "Invoices",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabDashboardMetrics_LabProfileId_MetricDate",
                schema: "Finance",
                table: "LabDashboardMetrics",
                columns: new[] { "LabProfileId", "MetricDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabOrderItems_LabOrderId",
                schema: "Patient",
                table: "LabOrderItems",
                column: "LabOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LabOrderItems_LabTestId",
                schema: "Patient",
                table: "LabOrderItems",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_LabOrders_LabProfileId",
                schema: "Patient",
                table: "LabOrders",
                column: "LabProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LabOrders_OrderCode",
                schema: "Patient",
                table: "LabOrders",
                column: "OrderCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabOrders_OrderedByDoctorId",
                schema: "Patient",
                table: "LabOrders",
                column: "OrderedByDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LabOrders_PatientId",
                schema: "Patient",
                table: "LabOrders",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabProfiles_ApplicationUserId",
                schema: "Patient",
                table: "LabProfiles",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabProfiles_RegistrationNumber",
                schema: "Patient",
                table: "LabProfiles",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabServiceListings_LabProfileId",
                schema: "Patient",
                table: "LabServiceListings",
                column: "LabProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_LabProfileId",
                schema: "Patient",
                table: "LabTests",
                column: "LabProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_TestCode",
                schema: "Patient",
                table: "LabTests",
                column: "TestCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorEntityId",
                schema: "Patients",
                table: "MedicalRecords",
                column: "DoctorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId",
                schema: "Patients",
                table: "MedicalRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                schema: "Patients",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_VisitDate",
                schema: "Patients",
                table: "MedicalRecords",
                column: "VisitDate");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineOrderItems_MedicineId",
                schema: "Inventory",
                table: "MedicineOrderItems",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineOrderItems_MedicineOrderId",
                schema: "Inventory",
                table: "MedicineOrderItems",
                column: "MedicineOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineOrders_OrderCode",
                schema: "Inventory",
                table: "MedicineOrders",
                column: "OrderCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicineOrders_OrderDate",
                schema: "Inventory",
                table: "MedicineOrders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineOrders_PatientId",
                schema: "Inventory",
                table: "MedicineOrders",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineOrders_PrescriptionId",
                schema: "Inventory",
                table: "MedicineOrders",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_GenericName",
                schema: "Inventory",
                table: "Medicines",
                column: "GenericName");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineCode",
                schema: "Inventory",
                table: "Medicines",
                column: "MedicineCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineName",
                schema: "Inventory",
                table: "Medicines",
                column: "MedicineName");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PharmacyProfileId",
                schema: "Inventory",
                table: "Medicines",
                column: "PharmacyProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineStocks_BatchNumber",
                schema: "Inventory",
                table: "MedicineStocks",
                column: "BatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineStocks_ExpiryDate",
                schema: "Inventory",
                table: "MedicineStocks",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineStocks_MedicineId",
                schema: "Inventory",
                table: "MedicineStocks",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineStocks_SupplierId",
                schema: "Inventory",
                table: "MedicineStocks",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_TemplateCode",
                schema: "Social",
                table: "NotificationTemplates",
                column: "TemplateCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtpCodes_UserId_Purpose_IsUsed",
                schema: "Identity",
                table: "OtpCodes",
                columns: new[] { "UserId", "Purpose", "IsUsed" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetRequests_TokenHash",
                schema: "Identity",
                table: "PasswordResetRequests",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetRequests_UserId_IsUsed",
                schema: "Identity",
                table: "PasswordResetRequests",
                columns: new[] { "UserId", "IsUsed" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferrals_PatientId",
                schema: "Patients",
                table: "PatientReferrals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferrals_ReferralCode",
                schema: "Patients",
                table: "PatientReferrals",
                column: "ReferralCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferrals_ReferredToDoctorId",
                schema: "Patients",
                table: "PatientReferrals",
                column: "ReferredToDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferrals_ReferringDoctorId",
                schema: "Patients",
                table: "PatientReferrals",
                column: "ReferringDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                schema: "Patients",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NationalId",
                schema: "Patients",
                table: "Patients",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientCode",
                schema: "Patients",
                table: "Patients",
                column: "PatientCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PhoneNumber",
                schema: "Patients",
                table: "Patients",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_PatientId",
                schema: "Patients",
                table: "PatientVitals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_RecordedAt",
                schema: "Patients",
                table: "PatientVitals",
                column: "RecordedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_BillId",
                schema: "Finance",
                table: "PaymentTransactions",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentCode",
                schema: "Finance",
                table: "PaymentTransactions",
                column: "PaymentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentDate",
                schema: "Finance",
                table: "PaymentTransactions",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_TransactionId",
                schema: "Finance",
                table: "PaymentTransactions",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyDashboardMetrics_PharmacyProfileId_MetricDate",
                schema: "Finance",
                table: "PharmacyDashboardMetrics",
                columns: new[] { "PharmacyProfileId", "MetricDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyProfiles_ApplicationUserId",
                schema: "Inventory",
                table: "PharmacyProfiles",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyProfiles_LicenseNumber",
                schema: "Inventory",
                table: "PharmacyProfiles",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyProfiles_PharmacyName",
                schema: "Inventory",
                table: "PharmacyProfiles",
                column: "PharmacyName");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyServiceListings_FeedStatus",
                schema: "Inventory",
                table: "PharmacyServiceListings",
                column: "FeedStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyServiceListings_PharmacyProfileId",
                schema: "Inventory",
                table: "PharmacyServiceListings",
                column: "PharmacyProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformWalletTransactions_PaymentId",
                schema: "Finance",
                table: "PlatformWalletTransactions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformWalletTransactions_TransactionAt",
                schema: "Finance",
                table: "PlatformWalletTransactions",
                column: "TransactionAt");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformWalletTransactions_TransactionCode",
                schema: "Finance",
                table: "PlatformWalletTransactions",
                column: "TransactionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlatformWalletTransactions_WalletId",
                schema: "Finance",
                table: "PlatformWalletTransactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_PostAudiences_PostId",
                schema: "Social",
                table: "PostAudiences",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_ParentCommentId",
                schema: "Social",
                table: "PostComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostInteractionId",
                schema: "Social",
                table: "PostComments",
                column: "PostInteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_UserId",
                schema: "Social",
                table: "PostComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostContents_PostId",
                schema: "Social",
                table: "PostContents",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostInteractions_PostId",
                schema: "Social",
                table: "PostInteractions",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostInteraction_User_Unique_Like",
                schema: "Social",
                table: "PostLikes",
                columns: new[] { "PostInteractionId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserId",
                schema: "Social",
                table: "PostLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostMedias_PostId",
                schema: "Social",
                table: "PostMedias",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostMetas_PostId",
                schema: "Social",
                table: "PostMetas",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostMetas_Status",
                schema: "Social",
                table: "PostMetas",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorUserId",
                schema: "Social",
                table: "Posts",
                column: "AuthorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_FeedItemId",
                schema: "Social",
                table: "Posts",
                column: "FeedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_LinkedFeedItemId",
                schema: "Social",
                table: "Posts",
                column: "LinkedFeedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInteraction_User_Unique_Save",
                schema: "Social",
                table: "PostSaves",
                columns: new[] { "PostInteractionId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostSaves_UserId",
                schema: "Social",
                table: "PostSaves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostShares_PostInteractionId",
                schema: "Social",
                table: "PostShares",
                column: "PostInteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_PostShares_UserId",
                schema: "Social",
                table: "PostShares",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Tag_Unique",
                schema: "Social",
                table: "PostTags",
                columns: new[] { "PostId", "Tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostMetaId",
                schema: "Social",
                table: "PostTags",
                column: "PostMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_MedicineId",
                schema: "Inventory",
                table: "PrescriptionItems",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_PrescriptionId",
                schema: "Inventory",
                table: "PrescriptionItems",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                schema: "Inventory",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                schema: "Inventory",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PrescribedDate",
                schema: "Inventory",
                table: "Prescriptions",
                column: "PrescribedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PrescriptionCode",
                schema: "Inventory",
                table: "Prescriptions",
                column: "PrescriptionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportEntities_GeneratedAt",
                schema: "Finance",
                table: "ReportEntities",
                column: "GeneratedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ReportEntities_ReportTitle",
                schema: "Finance",
                table: "ReportEntities",
                column: "ReportTitle");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignmentLogs_AssignedAt",
                schema: "Identity",
                table: "RoleAssignmentLogs",
                column: "AssignedAt");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignmentLogs_AssignedByUserId",
                schema: "Identity",
                table: "RoleAssignmentLogs",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignmentLogs_UserId",
                schema: "Identity",
                table: "RoleAssignmentLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Module_Action_Unique",
                schema: "Identity",
                table: "RolePermissions",
                columns: new[] { "Role", "Module", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffAttendances_StaffId_AttendanceDate",
                schema: "Finance",
                table: "StaffAttendances",
                columns: new[] { "StaffId", "AttendanceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_ApplicationUserId",
                schema: "Finance",
                table: "Staffs",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_DepartmentId",
                schema: "Finance",
                table: "Staffs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_StaffCode",
                schema: "Finance",
                table: "Staffs",
                column: "StaffCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_PhoneNumber",
                schema: "Inventory",
                table: "Suppliers",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierCode",
                schema: "Inventory",
                table: "Suppliers",
                column: "SupplierCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierName",
                schema: "Inventory",
                table: "Suppliers",
                column: "SupplierName");

            migrationBuilder.CreateIndex(
                name: "IX_TelemedicineSessions_AppointmentId",
                schema: "Patient",
                table: "TelemedicineSessions",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemedicineSessions_DoctorId_ScheduledAt",
                schema: "Patient",
                table: "TelemedicineSessions",
                columns: new[] { "DoctorId", "ScheduledAt" });

            migrationBuilder.CreateIndex(
                name: "IX_TelemedicineSessions_PatientId_ScheduledAt",
                schema: "Patient",
                table: "TelemedicineSessions",
                columns: new[] { "PatientId", "ScheduledAt" });

            migrationBuilder.CreateIndex(
                name: "IX_TelemedicineSessions_ScheduledAt",
                schema: "Patient",
                table: "TelemedicineSessions",
                column: "ScheduledAt");

            migrationBuilder.CreateIndex(
                name: "IX_TelemedicineSessions_SessionCode",
                schema: "Patient",
                table: "TelemedicineSessions",
                column: "SessionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelemedicineSessions_Status",
                schema: "Patient",
                table: "TelemedicineSessions",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginHistories_IpAddress",
                schema: "Identity",
                table: "UserLoginHistories",
                column: "IpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginHistories_UserId_LoginAt",
                schema: "Identity",
                table: "UserLoginHistories",
                columns: new[] { "UserId", "LoginAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_ReferenceId",
                schema: "Identity",
                table: "UserNotifications",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_UserId_IsRead_CreatedAt",
                schema: "Identity",
                table: "UserNotifications",
                columns: new[] { "UserId", "IsRead", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Module_Action_Override_Unique",
                schema: "Identity",
                table: "UserPermissionOverrides",
                columns: new[] { "UserId", "Module", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_Token",
                schema: "Identity",
                table: "UserRefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                schema: "Identity",
                table: "UserRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "Identity",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wards_WardCode",
                schema: "Ward",
                table: "Wards",
                column: "WardCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wards_WardName",
                schema: "Ward",
                table: "Wards",
                column: "WardName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminDashboardMetrics",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Admissions",
                schema: "Ward");

            migrationBuilder.DropTable(
                name: "AmbulanceDashboardMetrics",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AmbulanceServiceListings",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "AmbulanceWalletTransactions",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "AppointmentReminders",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "BedBookings",
                schema: "Ward");

            migrationBuilder.DropTable(
                name: "BillItems",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "ChatMessages",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "DashboardMetrics",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "DoctorAvailabilityLogs",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorDashboardMetrics",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "DoctorDocuments",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorEarnings",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorFeedbackSummaries",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorLeaves",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorNotes",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorNotifications",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorPerformanceReports",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorScheduleSlots",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "DoctorUnavailabilities",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "EmergencyVisits",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "Feedbacks",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "FeedItemLikes",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "FeedItemSaves",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "FeedItemTags",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "HealthLogs",
                schema: "Patients");

            migrationBuilder.DropTable(
                name: "HospitalSettings",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "InsuranceClaims",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "LabDashboardMetrics",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "LabOrderItems",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "LabServiceListings",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "MedicalRecords",
                schema: "Patients");

            migrationBuilder.DropTable(
                name: "MedicineOrderItems",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "MedicineStocks",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "NotificationTemplates",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "OtpCodes",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "PasswordResetRequests",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "PatientReferrals",
                schema: "Patients");

            migrationBuilder.DropTable(
                name: "PatientVitals",
                schema: "Patients");

            migrationBuilder.DropTable(
                name: "PharmacyDashboardMetrics",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "PharmacyServiceListings",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "PlatformWalletTransactions",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "PostAudiences",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostComments",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostContents",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostLikes",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostMedias",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostSaves",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostShares",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostTags",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PrescriptionItems",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "ReportEntities",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "RoleAssignmentLogs",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "StaffAttendances",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "TelemedicineSessions",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "UserLoginHistories",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserNotifications",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserPermissionOverrides",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AmbulanceProviderWallets",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Beds",
                schema: "Ward");

            migrationBuilder.DropTable(
                name: "Conversations",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "DoctorSchedules",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "AmbulanceBookings",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "LabOrders",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "LabTests",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "MedicineOrders",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "PaymentTransactions",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "PlatformWallets",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "PostInteractions",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PostMetas",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "Medicines",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Staffs",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "Wards",
                schema: "Ward");

            migrationBuilder.DropTable(
                name: "AmbulanceVehicles",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "LabProfiles",
                schema: "Patient");

            migrationBuilder.DropTable(
                name: "Prescriptions",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "PharmacyProfiles",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AmbulanceProviderProfiles",
                schema: "Inventory");

            migrationBuilder.DropTable(
                name: "Doctors",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "FeedItems",
                schema: "Social");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Staff");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "Patients");
        }
    }
}
