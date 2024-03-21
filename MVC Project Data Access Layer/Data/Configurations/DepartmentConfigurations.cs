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
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department> //install EF Core for SQL server provider to enable this interface
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //our Fluent API 
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(D=>D.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }
}
