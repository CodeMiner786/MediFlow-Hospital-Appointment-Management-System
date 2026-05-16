using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Lab
{
    public interface ILabTestRepository : IGenericRepository<LabTest>
    {
        // ১. একটি নির্দিষ্ট ল্যাবের অধীনে থাকা সব টেস্টের লিস্ট
        Task<IEnumerable<LabTest>> GetTestsByLabProfileIdAsync(Guid labProfileId);

        // ২. ক্যাটাগরি অনুযায়ী টেস্ট ফিল্টার করা (যেমন: Pathology, Radiology)
        Task<IEnumerable<LabTest>> GetTestsByCategoryAsync(string category);

        // ৩. টেস্ট কোড (TestCode) দিয়ে সরাসরি টেস্ট খুঁজে বের করা
        Task<LabTest?> GetByTestCodeAsync(string testCode, Guid labProfileId);

        // ৪. স্যাম্পল টাইপ অনুযায়ী টেস্ট ফিল্টার (যেমন: Blood, Urine)
        Task<IEnumerable<LabTest>> GetTestsBySampleTypeAsync(SampleType sampleType);

        // ৫. একটিভ এবং ইন-একটিভ টেস্টের সংখ্যা বের করা (Dashboard)
        Task<int> GetCountByStatusAsync(Guid labProfileId, bool isActive);

        // ৬. টেস্টের নাম দিয়ে সার্চ করা (Autocomplete এর জন্য)
        Task<IEnumerable<LabTest>> SearchTestsByNameAsync(string searchTerm, Guid labProfileId);
    }
}
