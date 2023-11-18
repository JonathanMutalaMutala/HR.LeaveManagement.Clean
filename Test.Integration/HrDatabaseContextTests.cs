using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Test.Integration
{
    public class HrDatabaseContextTests
    {
        private HrDatabaseContext _hrDatabaseContext;
        public HrDatabaseContextTests()
        {
            var DbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

                _hrDatabaseContext = new HrDatabaseContext(DbOptions);

        }


        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            // Arrange 
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            // Act 

           await  _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
           await _hrDatabaseContext.SaveChangesAsync();

            // Assert 
            leaveType.DateCreated.ShouldNotBeNull();

        }

        [Fact]
        public void Save_SetDateModifiedValue()
        {
            // Arrange 
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            // Act 

            _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            _hrDatabaseContext.SaveChangesAsync();

            // Assert 
            leaveType.DateModified.ShouldNotBeNull();

        }
    }
}