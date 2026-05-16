using HealthcareHospitalManagement.Domain.Enums.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Chat
{
    public class ConversationSummaryResponseDto
    {
        public Guid Id { get; set; }
        public ConversationType ConversationType { get; set; }
        public Guid OtherUserId { get; set; }
        public string OtherUserName { get; set; } = string.Empty;
        public string? OtherUserImageUrl { get; set; }
        public string OtherUserRole { get; set; } = string.Empty;
        public string? LastMessage { get; set; }
        public DateTime? LastMessageAt { get; set; }
        public bool IsLastMessageMine { get; set; }
        public int UnreadCount { get; set; }
        public bool IsActive { get; set; }
    }

}
