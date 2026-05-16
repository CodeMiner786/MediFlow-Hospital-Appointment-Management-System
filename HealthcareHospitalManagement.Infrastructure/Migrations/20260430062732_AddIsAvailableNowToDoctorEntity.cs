using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareHospitalManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableNowToDoctorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Identity",
                table: "OtpCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableNow",
                schema: "Staff",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Identity",
                table: "OtpCodes");

            migrationBuilder.DropColumn(
                name: "IsAvailableNow",
                schema: "Staff",
                table: "Doctors");
        }
    }
}
