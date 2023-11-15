using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    /// <summary>
    /// Cette classe Initialise le Mock pour la leaveType
    /// </summary>
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            //Arrange
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Name",

                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 10,
                    Name = "Test Marternity"
                }
            };

            // Arrange 
            var mockRepo = new Mock<ILeaveTypeRepository>();


            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            // Initialisation de la mop pour la creation d'un LeaveType
            mockRepo.Setup(r => r.IsLeaveTypeUnique(It.IsAny<string>()))
                .ReturnsAsync((string name) =>
                {
                    return !leaveTypes.Any(x => x.Name.ToLower() == name.ToLower());
                });




            return mockRepo;
        }
    }
}
