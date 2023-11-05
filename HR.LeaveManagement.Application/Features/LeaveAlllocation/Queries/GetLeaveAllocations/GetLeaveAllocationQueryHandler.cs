using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAlllocation.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationQueryHandler : IRequestHandler<GetLeaveAllocationQuery,List<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocationLst = await _leaveAllocationRepository.GetLeaveAllocationsWithDetailsLst();

            var data = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocationLst);


            return data;

        }
    }
}
