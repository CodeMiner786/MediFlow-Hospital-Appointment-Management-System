using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Doctor
{
    public interface IDoctorEarningRepository : IGenericRepository<DoctorEarning>
    {
        // নির্দিষ্ট একজন ডাক্তারের আয়ের হিস্ট্রি স্ট্রীম আকারে পাওয়া
        IAsyncEnumerable<DoctorEarning> GetEarningsByDoctorIdStream(Guid doctorId);

        // নির্দিষ্ট সময়ের ব্যবধানে (যেমন: এই মাসে) কত আয় হয়েছে তা স্ট্রীম করা
        IAsyncEnumerable<DoctorEarning> GetEarningsByDateRangeStream(Guid doctorId, DateTime start, DateTime end);

        // এখনো পেমেন্ট করা হয়নি (IsPaid = false) এমন আয়ের লিস্টগুলো খুঁজে বের করা
        IAsyncEnumerable<DoctorEarning> GetUnpaidEarningsStream(Guid doctorId);

        // অ্যাপয়েন্টমেন্ট আইডি দিয়ে সুনির্দিষ্ট আয়ের রেকর্ড খুঁজে বের করা
        Task<DoctorEarning?> GetByAppointmentIdAsync(Guid appointmentId);

        // মোট আয়ের সামারি (Total, Paid, Unpaid) বের করার জন্য মেথড
        Task<decimal> GetTotalDoctorShareAmountAsync(Guid doctorId, bool onlyPaid);
    }
}
