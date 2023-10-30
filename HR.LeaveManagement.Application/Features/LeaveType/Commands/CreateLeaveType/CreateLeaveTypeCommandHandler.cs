using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateleaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public  async Task<int> Handle(CreateleaveTypeCommand request, CancellationToken cancellationToken)
        {


            //Convertir to domain entity object 
            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);


            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);


            return leaveTypeToCreate.Id;

        }
    }
}
