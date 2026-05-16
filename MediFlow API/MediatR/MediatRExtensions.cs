using HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace MediFlow_API.MediatR;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
    {
        // ১. সরাসরি Application Layer-এর Assembly খুঁজে বের করা
        var applicationAssembly = typeof(GetUserProfileQueryHandler).Assembly;

        // ২. MediatR রেজিস্টার করা
        services.AddMediatR(cfg =>
        {
            // এই এক লাইন পুরো Application প্রোজেক্টের সব Handler, Command এবং Query খুঁজে নিবে
            cfg.RegisterServicesFromAssembly(applicationAssembly);
        });

        return services;
    }
}