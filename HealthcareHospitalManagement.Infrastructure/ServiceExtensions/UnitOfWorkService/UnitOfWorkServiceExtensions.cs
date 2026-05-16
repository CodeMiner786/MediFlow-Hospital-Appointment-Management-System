using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.ServiceExtensions.UnitOfWorkService
{
    public static class UnitOfWorkServiceExtensions
    {
        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
            services.AddScoped<IDoctorUnitOfWork, DoctorUnitOfWork>();
            services.AddScoped<IPatientUnitOfWork, PatientUnitOfWork>();
            services.AddScoped<IAppointmentUnitOfWork, AppointmentUnitOfWork>();
            services.AddScoped<ILabUnitOfWork, LabUnitOfWork>();
            services.AddScoped<IPharmacyUnitOfWork, PharmacyUnitOfWork>();
            services.AddScoped<IAmbulanceUnitOfWork, AmbulanceUnitOfWork>();
            services.AddScoped<IBillingPaymentUnitOfWork, BillingPaymentUnitOfWork>();
            services.AddScoped<IWardEmergencyUnitOfWork, WardEmergencyUnitOfWork>();
            services.AddScoped<ITelemedicineUnitOfWork, TelemedicineUnitOfWork>();
            services.AddScoped<IChatFeedUnitOfWork, ChatFeedUnitOfWork>();
            services.AddScoped<IDashboardUnitOfWork, DashboardUnitOfWork>();
            services.AddScoped<IAnalyticsNotificationUnitOfWork, AnalyticsNotificationUnitOfWork>();
            services.AddScoped<IFeedUnitOfWork, FeedUnitOfWork>();

            return services;
        }
    }
}
