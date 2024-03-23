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
                (Gender) => Gender.ToString(),
               (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true)
                );
        }
    }
}
