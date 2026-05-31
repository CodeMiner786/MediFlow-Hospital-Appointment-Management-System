using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? HeadDoctorName { get; set; }
        public string? ContactExtension { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
