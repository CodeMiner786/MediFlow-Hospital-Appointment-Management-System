using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Chat
{
    public class MarkMessageReadRequestDto
    {
        public Guid MessageId { get; set; }
        public Guid ConversationId { get; set; }
        public Guid ReadByUserId { get; set; }
    }

}
