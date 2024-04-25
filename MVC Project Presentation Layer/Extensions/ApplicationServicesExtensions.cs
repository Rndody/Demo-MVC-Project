using Microsoft.Extensions.DependencyInjection;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;

namespace MVC_Project_Presentation_Layer.Extensions
{
    public static class ApplicationServicesExtensions //static class as it will contain extension methods
    {
        //make extension method to add inside it the repository services we need to allow in the DI
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services; //we return the container of the DI so that we can call the extension methods in one line using . dot
                             //[no need to say services.MethodName each time we need to call a method -->  services.MethodName.AddScoped()]
        }


    }
}
