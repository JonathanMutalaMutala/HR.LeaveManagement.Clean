using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;
        private readonly IMapper _mapperMock;
        public CreateLeaveTypeCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });
            this._mapperMock = mapperConfig.CreateMapper();
            this._leaveTypeRepositoryMock = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        }

        [Fact]
        public async Task  Handle_ReturnBadRequest_LeaveTypeSameName()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapperMock, this._leaveTypeRepositoryMock.Object);

            await Assert.ThrowsAsync<BadRequestException>(async () => await handler.Handle(new CreateleaveTypeCommand() { Name = "Name", DefaultDays = 5 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ValidLeaveType()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapperMock, this._leaveTypeRepositoryMock.Object);

            await handler.Handle(new CreateleaveTypeCommand() { Name = "Test112", DefaultDays = 5 }, CancellationToken.None);

            var leaveTypes = await _leaveTypeRepositoryMock.Object.GetAsync();

            leaveTypes.Count.ShouldBe(4);

        }
    }
}
