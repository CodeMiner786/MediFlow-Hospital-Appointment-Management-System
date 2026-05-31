using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorNote
{
    public class CreateDoctorNoteDto
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid? AppointmentId { get; set; }
        public string NoteTitle { get; set; } = string.Empty;
        public string NoteContent { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = false;
        public bool IsPinned { get; set; } = false;
        public string? Tags { get; set; }
    }

}
