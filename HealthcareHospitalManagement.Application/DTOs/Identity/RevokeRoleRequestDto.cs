using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.Identity
{
    public class RevokeRoleRequestDto
    {
        public Guid TargetUserId { get; set; }
        public string? Reason { get; set; }
    }

}
