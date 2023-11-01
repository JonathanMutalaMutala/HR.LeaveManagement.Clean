using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validatorCommand = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);

            var validatorCommandResult = await validatorCommand.ValidateAsync(request);

            if(!validatorCommandResult.IsValid)
            {
                throw new BadRequestException("Invalid Leave Type", validatorCommandResult);
            }



            var leavetypeToUpdate = _mapper.Map<Domain.LeaveType>(request);


            await _leaveTypeRepository.UpdateAsync(leavetypeToUpdate);

            return Unit.Value;
        }
    }
}
