namespace HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine
{
    public enum MedicineCategory    { Tablet, Capsule, Syrup, Injection, Ointment, Drops, Inhaler, Patch }
    public enum PrescriptionStatus  { Pending, Dispensed, PartiallyDispensed, Cancelled }
    public enum StockStatus         { InStock, LowStock, OutOfStock, Expired }
    public enum MedicineOrderStatus { Placed, Confirmed, Preparing, Dispatched, Delivered, Cancelled }
    public enum SupplierStatus      { Active, Inactive, Blacklisted }
}
