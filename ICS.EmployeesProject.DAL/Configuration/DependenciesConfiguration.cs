using ICS.EmployeesProject.DAL.Interfaces.Repositories;
using ICS.EmployeesProject.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ICS.EmployeesProject.DAL.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return serviceCollection;
        }
    }
}
