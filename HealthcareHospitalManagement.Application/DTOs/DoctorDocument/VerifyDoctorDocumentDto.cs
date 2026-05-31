using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorDocument
{
    public class VerifyDoctorDocumentDto
    {
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

}
