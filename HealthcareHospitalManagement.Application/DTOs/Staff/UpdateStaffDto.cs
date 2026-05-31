using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Staff
{
    public class UpdateStaffDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? ProfileImageUrl { get; set; }
        public StaffType StaffType { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string Qualifications { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public ShiftType ShiftType { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
    }

}
