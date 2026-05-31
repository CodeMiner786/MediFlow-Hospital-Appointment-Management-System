using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorNote
{
    public class UpdateDoctorNoteDto
    {
        public string NoteTitle { get; set; } = string.Empty;
        public string NoteContent { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public bool IsPinned { get; set; }
        public string? Tags { get; set; }
    }

}
