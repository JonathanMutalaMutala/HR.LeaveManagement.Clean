using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocation;
using HR.LeaveManagement.BlazorUI.Services.Base;
using HR.LeaveManagement.BlazorUI.Models.Employee;

namespace HR.LeaveManagement.BlazorUI.MappingProfiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //LeaveType 
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDetailsDto, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateleaveTypeCommand, LeaveTypeVM>().ReverseMap();
            CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();


            //LeaveRequestListDto 
            CreateMap<LeaveRequestListDto, LeaveRequestVM>()
                // Permet de que lors du mapping il faut que DateRequested de LeaveRequestListDto qui provient de Service Client va Utiliser DateTime pour mapper Au lieu de Offset qui est mis par defaut
                .ForMember(x => x.DateRequested, opt => opt.MapFrom(a => a.DateRequested.DateTime))  
                .ForMember(x => x.StartDate, opt => opt.MapFrom(a => a.StartDate.DateTime))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(a => a.EndDate.DateTime))
                .ReverseMap();
            CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>()
                .ForMember(x => x.DateRequested, opt => opt.MapFrom(a => a.DateRequested.DateTime))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(a => a.StartDate.DateTime))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(a => a.EndDate.DateTime)).ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();


            //LeaveAllocation  
            CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocationDetailsDto, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();


            //Mapping Employee 
            CreateMap<EmployeeVM, Employee>().ReverseMap();



        }
    }
}
