using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "CBC48FE7-E16E-40A1-B691-90E51A6A23D9",
                    UserId = "ff2c1a69-9968-430f-9909-aee7d7baf13d"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "60EE54CF-992F-4E5E-81F1-6B18D71A15EB",
                     UserId = "4f7c7e09-e67a-4799-92be-9d6737754cf3"
                 });
        }
    }
}
