using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Data_Access_Layer.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E => E.Address).IsRequired();

            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(E => E.Gender).HasConversion
                (
                     (Gender) => Gender.ToString(), //to set in database => the value goes as string 
                     (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true) // to get  // parse it from string [as it comes from databae as string] to Gender
                );

            builder.Property(E => E.EmployeeType).HasConversion
                (
                    (EmployeeType) => EmployeeType.ToString(),
                    (employeeTypeAsString) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeTypeAsString, true)
                    //Parse return object so we cast it to the enum type [EmployeeType] // the true is for ignoring case sensitivity   
                );
        }
    }
}
