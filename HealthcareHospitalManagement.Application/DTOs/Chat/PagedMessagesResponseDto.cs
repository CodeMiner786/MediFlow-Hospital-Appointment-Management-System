using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Chat
{
    public class PagedMessagesResponseDto
    {
        public Guid ConversationId { get; set; }
        public IEnumerable<ChatMessageDetailResponseDto> Messages { get; set; } = [];
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasMore => TotalCount > PageNumber * PageSize;
    }

}
