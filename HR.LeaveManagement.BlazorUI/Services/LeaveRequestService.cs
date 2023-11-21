using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        public LeaveRequestService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, mapper, localStorageService)
        {
        }

        public Task ApproveLeaveRequest(int id, bool approved)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequestVM> GetLeaveRequestById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
