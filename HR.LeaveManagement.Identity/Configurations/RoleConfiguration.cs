using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configurations
{
    public  class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "60EE54CF-992F-4E5E-81F1-6B18D71A15EB",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",

                },
                new IdentityRole
                {
                    Id = "CBC48FE7-E16E-40A1-B691-90E51A6A23D9",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                });
        }
    }
}
