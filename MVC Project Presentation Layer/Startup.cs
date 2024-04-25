using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Data;
using MVC_Project_Data_Access_Layer.Models;
using MVC_Project_Presentation_Layer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project_Presentation_Layer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the DI container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            { options.UseSqlServer(Configuration.GetConnectionString("DefaultRndodyConnecton")); });

            services.AddControllersWithViews();

            #region Added from Extensions Folder 
            ///Deleted from here and added in the extension folder class
            ///services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            ///services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            ///call it through class
            ///  ApplicationServicesExtensions.AddApplicationServices(services); //call as static method

            ///call it as extension method
            services.AddApplicationServices();
            #endregion

            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            //when creating object fromclass implements  IMapper ... add our profile class to it 

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(2);

                //options.User.AllowedUserNameCharacters = "qwerttyuioplkjhhgffdazxcvbbnmm";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>//configurations for default schema  whatever what is the default one 
            {
                //options.LogoutPath = "";
                options.LoginPath = "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.AccessDeniedPath = /*"/Account/AccessDenied"*/"/Home/Error";
            }
            );
            services.AddAuthentication(options =>
            {
                //options.DefaultAuthenticateScheme = "hmbozo";//changed the default application schema

            })
               .AddCookie("hmbozo", options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.AccessDeniedPath = "/Home/Error";
            });

            #region Commented Code
            //services.AddScoped<ApplicationDbContext>();     
            //services.AddScoped<DbContextOptions<ApplicationDbContext>>();

            ///OR use one method instead of the above 2 methods for the DbContext
            // takes 3 parameters and the 3 have default values so no need to send them unless you need to change the default values

            /* the 1st parameter action void method DbContextOptionsBuilder which is in the DbContext class in the OnConfiguring method
             * which means we can send the connection string in this method instead of overriding the OnConfiguring method */
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
