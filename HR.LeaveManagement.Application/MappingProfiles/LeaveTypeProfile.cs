using AutoMapper;
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
        }
    }
}
