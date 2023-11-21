using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Query.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {

            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsList();
            var requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

            //Check if it is Logged in employee
            if(request.IsLoggingUser)
            {
                var userId = _userService.UserId;
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsByUserId(userId);

                var employee = await _userService.GetEmployeeByIdAsync(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsList();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests) ;
                foreach (var item in requests)
                {
                    item.Employee = await _userService.GetEmployeeByIdAsync(item.RequestingEmployedId);
                    
                }
            }





            return requests;
        }
    }
}
