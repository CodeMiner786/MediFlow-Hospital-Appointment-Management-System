using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Doctors
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Biography { get; set; }
        public string DoctorCode { get; set; } = string.Empty;
        public DoctorSpecialization Specialization { get; set; }
        public DoctorSpecialization? SubSpecialization { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime LicenseExpiryDate { get; set; }
        public int ExperienceYears { get; set; }
        public string Qualifications { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public decimal? TelemedicineConsultationFee { get; set; }
        public decimal? HomeVisitFee { get; set; }
        public decimal? CustomDoctorSharePercent { get; set; }
        public DoctorAvailabilityStatus AvailabilityStatus { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? OfficeExtension { get; set; }
        public bool IsAvailableNow { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int TotalAppointments { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

}
