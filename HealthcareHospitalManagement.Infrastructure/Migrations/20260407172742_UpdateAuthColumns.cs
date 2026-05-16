using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareHospitalManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuthColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_DoctorId_DayOfWeek_IsAvailable",
                schema: "Staff",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "Identity",
                table: "UserRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                schema: "Identity",
                table: "UserRefreshTokens",
                newName: "ExpiresAt");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "র্যান্ডমলি জেনারেটেড সিকিউর রিফ্রেশ টোকেন।");

            migrationBuilder.AlterColumn<string>(
                name: "JwtId",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "এক্সেস টোকেনের সাথে এই রিফ্রেশ টোকেনকে ম্যাপ করার জন্য JWT ID।");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRevoked",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClinicalSummary",
                schema: "Patients",
                table: "PatientReferrals",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                schema: "Staff",
                table: "DoctorSchedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "ডাক্তার যেখানে বসবেন (উদা: Room 402, Building A)।");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_JwtId",
                schema: "Identity",
                table: "UserRefreshTokens",
                column: "JwtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRefreshTokens_JwtId",
                schema: "Identity",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                schema: "Identity",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                schema: "Identity",
                table: "UserRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                schema: "Identity",
                table: "UserRefreshTokens",
                newName: "ExpiryDate");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "র্যান্ডমলি জেনারেটেড সিকিউর রিফ্রেশ টোকেন।",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "JwtId",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "এক্সেস টোকেনের সাথে এই রিফ্রেশ টোকেনকে ম্যাপ করার জন্য JWT ID।",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRevoked",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "Identity",
                table: "UserRefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ClinicalSummary",
                schema: "Patients",
                table: "PatientReferrals",
                type: "nvarchar(max)",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3000)",
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                schema: "Staff",
                table: "DoctorSchedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "ডাক্তার যেখানে বসবেন (উদা: Room 402, Building A)।",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId_DayOfWeek_IsAvailable",
                schema: "Staff",
                table: "DoctorSchedules",
                columns: new[] { "DoctorId", "DayOfWeek", "IsAvailable" });
        }
    }
}
