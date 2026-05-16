using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Enums.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Appointment
{
    public interface IAppointmentRepository : IGenericRepository<AppointmentEntity>
    {
        // অ্যাপয়েন্টমেন্ট কোড দিয়ে সুনির্দিষ্ট অ্যাপয়েন্টমেন্ট খুঁজে বের করা
        Task<AppointmentEntity?> GetByAppointmentCodeAsync(string appointmentCode);

        // নির্দিষ্ট একজন ডাক্তারের আজকের সব অ্যাপয়েন্টমেন্ট স্ট্রীম করা
        IAsyncEnumerable<AppointmentEntity> GetDoctorAppointmentsByDateStream(Guid doctorId, DateTime date);

        // নির্দিষ্ট একজন পেশেন্টের অ্যাপয়েন্টমেন্ট হিস্ট্রি স্ট্রীম করা
        IAsyncEnumerable<AppointmentEntity> GetPatientAppointmentHistoryStream(Guid patientId);

        // অ্যাপয়েন্টমেন্ট স্ট্যাটাস (যেমন: Scheduled, Cancelled) অনুযায়ী ফিল্টার করা
        IAsyncEnumerable<AppointmentEntity> GetAppointmentsByStatusStream(AppointmentStatus status);
        Task<bool> IsDoctorBookedAsync(Guid doctorId, DateTime appointmentDate, TimeOnly startTime, CancellationToken cancellationToken);

    }
}
