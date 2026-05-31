using HealthcareHospitalManagement.Domain.Common.PagedResponse;
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
        : GenericRepository<DepartmentEntity>(context), IDepartmentRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<DepartmentEntity> _dbSet = context.Set<DepartmentEntity>();

        // ইউনিক ডিপার্টমেন্ট কোড এবং সফট ডিলিট চেক করে ডাটা আনা হচ্ছে
        public async Task<DepartmentEntity?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.Code == code && !d.IsDeleted);
        }

        // নাম অনুযায়ী ডিপার্টমেন্ট খুঁজে বের করা
        public async Task<DepartmentEntity?> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(d => d.Name == name && !d.IsDeleted);
        }

        // নির্দিষ্ট লোকেশন অনুযায়ী সব ডিপার্টমেন্ট স্ট্রীম আকারে পাঠানো হচ্ছে
        public IAsyncEnumerable<DepartmentEntity> GetDepartmentsByLocationStream(string location)
        {
            return _dbSet
                .Where(d => d.Location == location && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডিপার্টমেন্টের তথ্যের সাথে ডাক্তারদের ডাটাও ইনক্লুড করা হচ্ছে (Eager Loading)
        public async Task<DepartmentEntity?> GetDepartmentWithDoctorsAsync(Guid departmentId)
        {
            return await _dbSet
                .Include(d => d.Doctors)
                .FirstOrDefaultAsync(d => d.Id == departmentId && !d.IsDeleted);
        }

        public async Task<PagedResponse<DepartmentEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _dbSet.Where(d => !d.IsDeleted).AsNoTracking();

            var totalCount = await query.CountAsync(ct);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            // Constructor না ডেকে static factory ব্যবহার করো
            return PagedResponse<DepartmentEntity>.Create(items, totalCount, pageNumber, pageSize);
        }

    }
}
