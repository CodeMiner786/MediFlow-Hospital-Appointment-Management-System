using HealthcareHospitalManagement.Domain.Entities.Doctor;
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
    // Primary Constructor ব্যবহার করে ApplicationDbContext কে বেস ক্লাসে ইনজেক্ট করা হয়েছে
    public class DepartmentRepository(ApplicationDbContext context)
        : GenericRepository<Department>(context), IDepartmentRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<Department> _dbSet = context.Set<Department>();

        // ইউনিক ডিপার্টমেন্ট কোড এবং সফট ডিলিট চেক করে ডাটা আনা হচ্ছে
        public async Task<Department?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.Code == code && !d.IsDeleted);
        }

        // নাম অনুযায়ী ডিপার্টমেন্ট খুঁজে বের করা
        public async Task<Department?> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.Name == name && !d.IsDeleted);
        }

        // নির্দিষ্ট লোকেশন অনুযায়ী সব ডিপার্টমেন্ট স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<Department> GetDepartmentsByLocationStream(string location)
        {
            return _dbSet
                .Where(d => d.Location == location && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডিপার্টমেন্টের তথ্যের সাথে ডাক্তারদের ডাটাও ইনক্লুড করা হচ্ছে (Eager Loading)
        public async Task<Department?> GetDepartmentWithDoctorsAsync(Guid departmentId)
        {
            return await _dbSet
                .Include(d => d.Doctors)
                .FirstOrDefaultAsync(d => d.Id == departmentId && !d.IsDeleted);
        }
    }
}
