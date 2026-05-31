using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorNote
{
    public class DoctorNoteDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; } = string.Empty;
        public Guid? AppointmentId { get; set; }
        public string NoteTitle { get; set; } = string.Empty;
        public string NoteContent { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public bool IsPinned { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
