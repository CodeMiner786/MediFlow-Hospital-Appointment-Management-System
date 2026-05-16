using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.DatabaseContext.Extensions
{
    public static class ModelBuilderExtensions
    {
        // this , keyword allows us to call this method directly on the modelBuilder instance in OnModelCreating
        public static void ConfigureHospitalManagementModels(this ModelBuilder modelBuilder) 
        {
            // Apply configurations for all entities in the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModelBuilderExtensions).Assembly);
        }
    }
}
