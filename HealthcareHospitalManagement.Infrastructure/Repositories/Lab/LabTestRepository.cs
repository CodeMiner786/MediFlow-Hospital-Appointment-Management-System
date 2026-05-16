using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Lab;
using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Lab
{
    public class LabTestRepository(ApplicationDbContext context)
        : GenericRepository<LabTest>(context), ILabTestRepository
    {
        private readonly DbSet<LabTest> _dbSet = context.Set<LabTest>();

        // 🔹 ল্যাব প্রোফাইল অনুযায়ী সব টেস্ট লোড করা
        public async Task<IEnumerable<LabTest>> GetTestsByLabProfileIdAsync(Guid labProfileId)
        {
            return await _dbSet
                .Where(t => t.LabProfileId == labProfileId && !t.IsDeleted)
                .OrderBy(t => t.TestName)
                .ToListAsync();
        }

        // 🔹 ক্যাটাগরি ভিত্তিক সার্চ (যেমন: সব ব্লাড টেস্ট একসাথে দেখা)
        public async Task<IEnumerable<LabTest>> GetTestsByCategoryAsync(string category)
        {
            return await _dbSet
                .Where(t => t.Category.ToLower() == category.ToLower() && t.IsActive && !t.IsDeleted)
                .ToListAsync();
        }

        // 🔹 ইউনিক টেস্ট কোড দিয়ে ফিল্টার
        public async Task<LabTest?> GetByTestCodeAsync(string testCode, Guid labProfileId)
        {
            return await _dbSet.FirstOrDefaultAsync(t =>
                t.TestCode == testCode &&
                t.LabProfileId == labProfileId &&
                !t.IsDeleted);
        }

        // 🔹 স্যাম্পল টাইপ অনুযায়ী কুয়েরি (ল্যাব টেকনিশিয়ানদের কাজের সুবিধার জন্য)
        public async Task<IEnumerable<LabTest>> GetTestsBySampleTypeAsync(SampleType sampleType)
        {
            return await _dbSet
                .Where(t => t.SampleType == sampleType && t.IsActive && !t.IsDeleted)
                .ToListAsync();
        }

        // 🔹 ড্যাশবোর্ডের জন্য একটিভ/ইন-একটিভ টেস্ট কাউন্ট
        public async Task<int> GetCountByStatusAsync(Guid labProfileId, bool isActive)
        {
            return await _dbSet.CountAsync(t =>
                t.LabProfileId == labProfileId &&
                t.IsActive == isActive &&
                !t.IsDeleted);
        }

        // 🔹 ডক্টর যখন অর্ডারের সময় টাইপ করবে, তখন দ্রুত টেস্ট খুঁজে পাওয়া
        public async Task<IEnumerable<LabTest>> SearchTestsByNameAsync(string searchTerm, Guid labProfileId)
        {
            return await _dbSet
                .Where(t => t.LabProfileId == labProfileId &&
                            t.TestName.Contains(searchTerm) &&
                            t.IsActive &&
                            !t.IsDeleted)
                .Take(10) // পারফরম্যান্সের জন্য লিমিট দেওয়া হলো
                .ToListAsync();
        }
    }
}
