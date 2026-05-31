using HealthcareHospitalManagement.Domain.Enums.Notification;
using HealthcareHospitalManagement.Domain.Enums.NotificationPriority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorNotification
{
    public class UpdateDoctorNotificationDto
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationCategory Category { get; set; }
        public string? ActionUrl { get; set; }
        public bool IsUrgent { get; set; }
        public NotificationPriorityTypes Priority { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

}
