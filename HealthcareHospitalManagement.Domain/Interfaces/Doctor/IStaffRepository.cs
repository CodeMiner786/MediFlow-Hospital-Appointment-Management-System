using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IStaffRepository : IGenericRepository<StaffEntity>
    {
        // স্টাফ কোড (STAFF-101) দিয়ে নির্দিষ্ট স্টাফকে খুঁজে বের করা
        Task<StaffEntity?> GetByStaffCodeAsync(string staffCode);

        // নির্দিষ্ট ধরণের স্টাফ (যেমন: Nurse, Admin, Receptionist) স্ট্রীম করা
        IAsyncEnumerable<StaffEntity> GetStaffByTypeStream(StaffType staffType);

        // একটি নির্দিষ্ট ডিপার্টমেন্টের সব স্টাফদের খুঁজে বের করা
        IAsyncEnumerable<StaffEntity> GetStaffByDepartmentStream(Guid departmentId);

        // স্যালারি রেঞ্জ অনুযায়ী স্টাফদের ফিল্টার করা (HR/Admin প্যানেলের জন্য)
        IAsyncEnumerable<StaffEntity> GetStaffBySalaryRangeStream(decimal minSalary, decimal maxSalary);

        // নির্দিষ্ট শিফটে (Morning/Night) কর্মরত স্টাফদের লিস্ট আনা
        IAsyncEnumerable<StaffEntity> GetStaffByShiftStream(ShiftType shiftType);

        // স্টাফের এটেনডেন্স এবং ডিপার্টমেন্টসহ বিস্তারিত তথ্য লোড করা
        Task<StaffEntity?> GetStaffWithDetailsAsync(Guid staffId);
    }
}
