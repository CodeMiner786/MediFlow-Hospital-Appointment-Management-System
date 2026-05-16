using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.Enums.Audit;
using HealthcareHospitalManagement.Domain.Enums.UserRole; // Domain layer namespace

namespace HealthcareHospitalManagement.Application.DTOs.Identity;
public class AuditLogFilterRequestDto : BaseFilterDto
{
    public Guid? UserId { get; set; }

    // সরাসরি এনাম টাইপ
    public AuditAction? Action { get; set; }

    public UserRole? UserRole { get; set; }

    public string? EntityName { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
   
}