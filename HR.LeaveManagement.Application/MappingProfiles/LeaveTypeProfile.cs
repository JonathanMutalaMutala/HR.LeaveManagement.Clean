using AutoMapper;
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
            //Je peux faire les mappages dans le deux sens de mon Dto à mon Entity ou de mon Entity à mon Dto
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>(); // Je ne reverse pas puisque je vais aller de LeaveType et convertir en LeaveTypeDetailsDto

        }
    }
}
