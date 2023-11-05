using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAlllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAlllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{

    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            // Si le mapping ne sait fait pas entre le Dto et la classe d'entité j'aurai une erreur 
            //Je peux faire les mappages dans le deux sens de mon Dto à mon Entity ou de mon Entity à mon Dto
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>(); // Je ne reverse pas puisque je vais aller de LeaveType et convertir en LeaveTypeDetailsDto garder l'original
            CreateMap<CreateleaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>();


            // Allocation 
            CreateMap<LeaveAllocationDetailsDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDto>(); 
        }
    }
}
