using HealthcareHospitalManagement.Domain.Enums.Notification;
using HealthcareHospitalManagement.Domain.Enums.NotificationPriority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorNotification
{
    public class DoctorNotificationDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationCategory Category { get; set; }
        public Guid? ReferenceId { get; set; }
        public string? ReferenceType { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? ActionUrl { get; set; }
        public bool IsUrgent { get; set; }
        public NotificationPriorityTypes Priority { get; set; }
        public DateTime DeliveredAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
