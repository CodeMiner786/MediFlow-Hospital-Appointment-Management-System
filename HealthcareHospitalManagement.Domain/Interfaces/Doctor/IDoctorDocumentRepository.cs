using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Enums.DoctorDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorDocumentRepository : IGenericRepository<DoctorDocument>
    {
        // নির্দিষ্ট একজন ডাক্তারের সব ডকুমেন্ট স্ট্রীম আকারে পাওয়া
        IAsyncEnumerable<DoctorDocument> GetDocumentsByDoctorIdStream(Guid doctorId);

        // ভেরিফিকেশন স্ট্যাটাস অনুযায়ী ডকুমেন্টগুলো ফিল্টার করা (অ্যাডমিন প্যানেলের জন্য)
        IAsyncEnumerable<DoctorDocument> GetDocumentsByVerificationStatusStream(bool isVerified);

        // নির্দিষ্ট ধরণের ডকুমেন্ট (যেমন: License, Degree) খুঁজে বের করা
        IAsyncEnumerable<DoctorDocument> GetDocumentsByTypeStream(DoctorDocumentType type);

        // যেসব সার্টিফিকেটের মেয়াদ শেষ হয়ে গেছে বা শেষ হতে চলেছে সেগুলো খুঁজে বের করা
        IAsyncEnumerable<DoctorDocument> GetExpiringDocumentsStream(DateTime thresholdDate);
    }
}
