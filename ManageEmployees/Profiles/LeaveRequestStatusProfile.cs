using AutoMapper;
using ManageEmployees.Dtos.LeaveRequestStatus;
using ManageEmployees.Entities;

namespace ManageEmployees.Profiles
{
    public class LeaveRequestStatusProfile : Profile
    {
        public LeaveRequestStatusProfile()
        {
            CreateMap<LeaveRequestStatus, ReadLeaveRequestStatus>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LeaveRequestStatusId));
            CreateMap<CreateLeaveRequestStatus, LeaveRequestStatus>();
        }
    }
}