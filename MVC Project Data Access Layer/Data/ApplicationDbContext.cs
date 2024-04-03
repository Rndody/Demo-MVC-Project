using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Project_Data_Access_Layer.Data.Configurations;
using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Data_Access_Layer.Data
{
    public class ApplicationDbContext : /*DbContext*/ IdentityDbContext
    {
        ///DbSets
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        ///Constructors
        /*   public ApplicationDbContext():base(new DbContextOptions<ApplicationDbContext>())
           {
               //so we won't use this way in creating new objec 
               // allow the DI so that CLR creates the object 
           }*/

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        ///Methods
        //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("server= .; Database=MVCApplicationDemo; Trusted_Connection=True");

        /*any request send from sql server service by default itcontains One Query that returns only one result set 
         if you need the request send from application to database to contain more than one Query [return more than one result set] =>  MultipleActiveResultSets= True  */



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles"); //to change the name of the table created 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations()); //adding them one by one
        //modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfigurations()); //adding them one by one

    }
}
