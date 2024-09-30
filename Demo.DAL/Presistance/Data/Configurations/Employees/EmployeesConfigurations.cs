using Demo.DAL.Common.Enums;
using Demo.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Data.Configurations.Employees
{
    internal class EmployeesConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            //Convert the Enums to string in the database and convert into Enums when receiving form the database

            builder.Property(E => E.Gender).HasConversion(
                    (gender) => gender.ToString(),
                    (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );

            builder.Property(E => E.EmployeeType).HasConversion(
                    (type) => type.ToString(),
                    (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)
                  );

            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()"); // Create the time on creating the record
        }
    }
}
