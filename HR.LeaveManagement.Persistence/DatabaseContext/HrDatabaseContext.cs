using HR.LeaveManagement.Application.Identity;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.DatabaseContext
{
    public class HrDatabaseContext : DbContext
    {
        private readonly DbContextOptions<HrDatabaseContext> options;
        private readonly IUserService _userService;

        public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options,IUserService userService): base(options) 
        {
            this.options = options;
            this._userService = userService;
        }
        public DbSet<LeaveType> LeaveTypes { get; set; }    
        public DbSet<LeaveAllocation> leaveAllocations { get; set; }
        public DbSet<LeaveRequest> leaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                entry.Entity.ModifiedBy = _userService.UserId;
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = _userService.UserId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
