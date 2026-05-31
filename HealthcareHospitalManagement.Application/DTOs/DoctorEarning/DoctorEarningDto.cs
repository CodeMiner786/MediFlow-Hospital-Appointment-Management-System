using HealthcareHospitalManagement.Domain.Enums.DoctorEarning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.DTOs.DoctorEarning
{
    public class DoctorEarningDto
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = string.Empty;
        public Guid? AppointmentId { get; set; }
        public Guid? TelemedicineSessionId { get; set; }
        public DateTime EarningDate { get; set; }
        public decimal TotalFee { get; set; }
        public decimal HospitalSharePercent { get; set; }
        public decimal HospitalShareAmount { get; set; }
        public decimal DoctorShareAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidAt { get; set; }
        public string? PaymentReference { get; set; }
        public DoctorEarningType EarningType { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
