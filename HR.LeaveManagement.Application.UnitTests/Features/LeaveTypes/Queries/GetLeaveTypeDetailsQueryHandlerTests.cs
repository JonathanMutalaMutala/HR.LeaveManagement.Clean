using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeDetailsQueryHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;
        private readonly IMapper _mapperMock;

        public GetLeaveTypeDetailsQueryHandlerTests()
        {
            _leaveTypeRepositoryMock = new Mock<ILeaveTypeRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });
            _mapperMock = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFoundException_WhenDataNotExist()
        {
            // Arrange 
            var query = new GetLeaveTypeDetailsQuery(5);


            var handler = new GetLeaveTypeDetailsQueryHandler(_mapperMock, _leaveTypeRepositoryMock.Object);

      
            // Assert 
          await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None)); 
        }

        [Fact]
        public async Task Handle_Should_ReturnLeaveType_WhenDataExist()
        {
            // Arrange 
            var query = new GetLeaveTypeDetailsQuery(1);

            var leaveTypeData = new LeaveType()
            {
                Id = 1,
                Name = "Name",
                DefaultDays = 10,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now.AddDays(2)
            };

            _leaveTypeRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                                    .ReturnsAsync(leaveTypeData);

            var handler = new GetLeaveTypeDetailsQueryHandler(_mapperMock, _leaveTypeRepositoryMock.Object);

            // Act 
            var result = await handler.Handle(query, CancellationToken.None); 


            //Assert
            result.ShouldNotBeNull();
        }
    }
}
