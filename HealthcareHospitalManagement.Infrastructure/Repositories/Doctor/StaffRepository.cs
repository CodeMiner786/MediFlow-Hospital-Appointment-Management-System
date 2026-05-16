using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Doctor
{
    // Primary Constructor ব্যবহার করে ApplicationDbContext ইনজেক্ট করা হয়েছে
    public class StaffRepository(ApplicationDbContext context)
        : GenericRepository<StaffEntity>(context), IStaffRepository
    {
        private readonly DbSet<StaffEntity> _dbSet = context.Set<StaffEntity>();

        // ইউনিক স্টাফ কোড দিয়ে সার্চ
        public async Task<StaffEntity?> GetByStaffCodeAsync(string staffCode)
        {
            return await _dbSet
                .FirstOrDefaultAsync(s => s.StaffCode == staffCode && !s.IsDeleted);
        }

        // স্টাফ টাইপ (যেমন: Nurse) অনুযায়ী ফিল্টার করে স্ট্রীম করা
        public IAsyncEnumerable<StaffEntity> GetStaffByTypeStream(StaffType staffType)
        {
            return _dbSet
                .Where(s => s.StaffType == staffType && !s.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডিপার্টমেন্ট অনুযায়ী স্টাফ লিস্ট
        public IAsyncEnumerable<StaffEntity> GetStaffByDepartmentStream(Guid departmentId)
        {
            return _dbSet
                .Where(s => s.DepartmentId == departmentId && !s.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // বেতনের সীমা অনুযায়ী ডাটা ফিল্টার করা
        public IAsyncEnumerable<StaffEntity> GetStaffBySalaryRangeStream(decimal minSalary, decimal maxSalary)
        {
            return _dbSet
                .Where(s => s.Salary >= minSalary && s.Salary <= maxSalary && !s.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // শিফট অনুযায়ী (Morning/Night) স্টাফদের তালিকা
        public IAsyncEnumerable<StaffEntity> GetStaffByShiftStream(ShiftType shiftType)
        {
            return _dbSet
                .Where(s => s.ShiftType == shiftType && !s.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // স্টাফের প্রোফাইল ভিউ করার জন্য ডিপার্টমেন্ট এবং এটেনডেন্স রেকর্ডসহ লোড করা
        public async Task<StaffEntity?> GetStaffWithDetailsAsync(Guid staffId)
        {
            return await _dbSet
                .Include(s => s.Department)
                .Include(s => s.Attendances.OrderByDescending(a => a.AttendanceDate).Take(30)) // সাম্প্রতিক ৩০ দিনের এটেনডেন্স
                .FirstOrDefaultAsync(s => s.Id == staffId && !s.IsDeleted);
        }
    }
}
