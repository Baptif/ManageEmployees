using AutoMapper;
using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Profiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest, ReadLeaveRequest>();
            CreateMap<CreateLeaveRequest, LeaveRequest>();
        }
    }
}