using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    /// <summary>
    /// Cette classe se charge de faire le Test pour le UpdateLeaveTypeHandler
    /// </summary>
    public class UpdateLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;
        private readonly IMapper _mapperMock;
        private readonly Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> _loggerMock;

        public UpdateLeaveTypeCommandHandlerTests()
        {
            var mapper = new MapperConfiguration(x =>
            {
                x.AddProfile<LeaveTypeProfile>();
            });
            _mapperMock = mapper.CreateMapper();
            this._leaveTypeRepositoryMock = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            this._loggerMock = new Mock<IAppLogger<UpdateLeaveTypeCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ValideLeaveType_ToUpdate()
        {

            var command = new UpdateLeaveTypeCommand()
            {
                Name = "New name By Jonathan",
                Id = 1,
                DefaultDays = 10
            };

            var handler = new UpdateLeaveTypeCommandHandler(_mapperMock, _leaveTypeRepositoryMock.Object,_loggerMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            _leaveTypeRepositoryMock.Verify(x => x.GetByIdAsync(command.Id), Times.Once);
            Assert.Equal(Unit.Value,result);
        }


        [Fact]
        public async Task Handle_UpdateWrongLeaveType_ReturnBadRequestException()
        {
            var command = new UpdateLeaveTypeCommand()
            {
                Id = 990,
                DefaultDays = 10,
                Name = "Hello Testing"
            };

            var handler = new UpdateLeaveTypeCommandHandler(_mapperMock, _leaveTypeRepositoryMock.Object, _loggerMock.Object);

            await Assert.ThrowsAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_NullLeaveTypeName_ReturnBadRequestException()
        {
            var command = new UpdateLeaveTypeCommand()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Name"
            };
            var test = new UpdateLeaveTypeCommandValidator(_leaveTypeRepositoryMock.Object);

            var testValidation = await test.ValidateAsync(command);

            //var handler = new UpdateLeaveTypeCommandHandler(_mapperMock, _leaveTypeRepositoryMock.Object, _loggerMock.Object);


            //await Assert.ThrowsAsync<BadRequestException>(async () => await handler.Handle(command, CancellationToken.None));
            Assert.False(testValidation.IsValid);
        }
    }
}
