using HealthcareHospitalManagement.Domain.Enums.DoctorDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorDocument
{
    public class DoctorDocumentDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public DoctorDocumentType DocumentType { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string? FileType { get; set; }
        public long? FileSizeBytes { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? VerifiedBy { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
