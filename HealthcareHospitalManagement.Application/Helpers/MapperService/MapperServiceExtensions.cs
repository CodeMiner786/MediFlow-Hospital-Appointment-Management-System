using AutoMapper;
using HealthcareHospitalManagement.Application.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Helpers.MapperService;

public static class MapperServiceExtensions
{
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        // AutoMapper এর built-in registration
        // এখানে MappingProfile class থেকে সব mapping rules load হবে
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        return services;
    }
}
