using ICS.EmployeesProject.BL.Interfaces.Services;
using ICS.EmployeesProject.BL.Mapping;
using ICS.EmployeesProject.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ICS.EmployeesProject.BL.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
            serviceCollection.AddScoped<IProcessService, ProcessService>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterMappingConfig(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(
                c => c.AddProfile<MappingConfiguration>(),
                typeof(MappingConfiguration));

            return serviceCollection;
        }
    }
}
