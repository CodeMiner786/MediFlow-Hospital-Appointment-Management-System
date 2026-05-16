using HealthcareHospitalManagement.Domain.Enums.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Chat
{
    public class UpdateMessageStatusRequestDto
    {
        public Guid MessageId { get; set; }
        public MessageStatus Status { get; set; }
    }

}
