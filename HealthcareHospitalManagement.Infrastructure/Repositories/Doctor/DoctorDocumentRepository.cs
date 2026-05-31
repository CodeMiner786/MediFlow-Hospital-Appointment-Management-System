using HealthcareHospitalManagement.Domain.Common.PagedResponse;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorDocument;
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
    public class DoctorDocumentRepository(ApplicationDbContext context)
        : GenericRepository<DoctorDocument>(context), IDoctorDocumentRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল তৈরি করা হয়েছে
        private readonly DbSet<DoctorDocument> _dbSet = context.Set<DoctorDocument>();

        // ডাক্তারের আইডি অনুযায়ী তার সব আপলোড করা ফাইল স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorDocument> GetDocumentsByDoctorIdStream(Guid doctorId)
        {
            return _dbSet
                .Where(d => d.DoctorId == doctorId && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // অ্যাডমিনদের ভেরিফিকেশন করার সুবিধার জন্য পেন্ডিং বা ভেরিফাইড ডাটা ফিল্টার করা
        public IAsyncEnumerable<DoctorDocument> GetDocumentsByVerificationStatusStream(bool isVerified)
        {
            return _dbSet
                .Where(d => d.IsVerified == isVerified && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ডকুমেন্টের টাইপ (License, NID, Certificate) অনুযায়ী ফিল্টার করা
        public IAsyncEnumerable<DoctorDocument> GetDocumentsByTypeStream(DoctorDocumentType type)
        {
            return _dbSet
                .Where(d => d.DocumentType == type && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // এক্সপায়ারি ডেট চেক করে মেয়াদোত্তীর্ণ ডকুমেন্টগুলো স্ট্রীম করা হচ্ছে
        public IAsyncEnumerable<DoctorDocument> GetExpiringDocumentsStream(DateTime thresholdDate)
        {
            return _dbSet
                .Where(d => d.ExpiryDate <= thresholdDate && !d.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        // ✅ নতুন: Pagination সহ ডকুমেন্ট আনা
        public async Task<PagedResponse<DoctorDocument>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _dbSet.Where(d => !d.IsDeleted).AsNoTracking();

            var totalCount = await query.CountAsync(ct);
            var items = await query
                .OrderByDescending(d => d.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return PagedResponse<DoctorDocument>.Create(items, totalCount, pageNumber, pageSize);
        }
    }
}
