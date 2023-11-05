using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Query.GetLeaveRequestList;
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

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        { 
            var leaveRequest = _mapper.Map<LeaveRequestDetailsDto>(await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));


            return leaveRequest;
        }
    }
}
