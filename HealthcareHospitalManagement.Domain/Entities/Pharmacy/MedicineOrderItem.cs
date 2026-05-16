using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Pharmacy
{
    public class MedicineOrderItem : BaseEntity
    {
        // Foreign Key
        public      Guid                MedicineOrderId         { get; set; }
        public      MedicineOrder       MedicineOrder           { get; set; } = null!;

        public      Guid                MedicineId              { get; set; }
        public      Medicine            Medicine                { get; set; } = null!;

        public      int                 Quantity                { get; set; }
        public      decimal             UnitPrice               { get; set; }
        public      decimal             Discount                { get; set; }
        public      decimal             TotalPrice              { get; set; } 
    }
}