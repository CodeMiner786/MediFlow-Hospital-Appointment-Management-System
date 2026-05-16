using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.OTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IOtpRepository : IGenericRepository<OtpCode>
    {
        // ১. ইউজারের জন্য লেটেস্ট এবং ভ্যালিড OTP খুঁজে বের করা
        Task<OtpCode?> GetLatestValidOtpAsync(Guid userId, OtpPurpose purpose);

        // ২. পুরানো বা এক্সপায়ারড OTP গুলো ইনভ্যালিড করে দেওয়া (Security)
        Task InvalidateOldOtpsAsync(Guid userId, OtpPurpose purpose);

        // ৩. ভুল অ্যাটেম্পট সংখ্যা বাড়ানো
        Task IncrementAttemptAsync(Guid otpId);

        // ৪. OTP ব্যবহার হয়ে গেলে স্ট্যাটাস আপডেট করা
        Task MarkAsUsedAsync(Guid otpId);

        // ৫. ক্লিনআপ: অনেক পুরানো (যেমন ১ দিন আগের) OTP ডাটাবেজ থেকে মুছে ফেলা
        Task DeleteExpiredOtpsAsync();
    }
}
