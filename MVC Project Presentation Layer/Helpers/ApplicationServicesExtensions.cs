using Microsoft.Extensions.DependencyInjection;
using MVC_Project_Business_Logic_Layer;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;

namespace MVC_Project_Presentation_Layer.Helpers
{
    public static class ApplicationServicesExtensions //static class as it will contain extension methods
    {
        //make extension method to add inside it the repository services we need to allow in the DI
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            ///objects from those classes are not needed any more in the DI container we created them in the UnitOfWork class 
            ///services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            ///services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services; //we return the container of the DI so that we can call the extension methods in one line using . dot
                             //[no need to say services.MethodName each time we need to call a method -->  services.MethodName.AddScoped()]
        }


    }
}
