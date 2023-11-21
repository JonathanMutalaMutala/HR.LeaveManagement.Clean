using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Query.GetLeaveRequestList;
using HR.LeaveManagement.Application.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Query.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler :IRequestHandler<GetLeaveRequestDetailQuery,LeaveRequestDetailsDto>
    {
        public readonly ILeaveRequestRepository _leaveRequestRepository;
        public readonly IMapper _mapper;
        public readonly IUserService _userService;

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        { 
            var leaveRequest = _mapper.Map<LeaveRequestDetailsDto>(await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));

            if(leaveRequest == null)
            {
                throw new NotFoundException(nameof(LeaveRequest), request.Id);
            }

            //Add employee details 

            leaveRequest.Employee = await _userService.GetEmployeeByIdAsync(_userService.UserId);  

            return leaveRequest;
        }
    }
}
