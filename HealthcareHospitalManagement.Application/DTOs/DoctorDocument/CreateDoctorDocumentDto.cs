using HealthcareHospitalManagement.Domain.Enums.DoctorDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorDocument
{
    public class CreateDoctorDocumentDto
    {
        public Guid DoctorId { get; set; }
        public DoctorDocumentType DocumentType { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string? FileType { get; set; }
        public long? FileSizeBytes { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Notes { get; set; }
    }

}
