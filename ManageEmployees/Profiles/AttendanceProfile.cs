using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;

namespace ManageEmployees.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, ReadAttendance>();
            CreateMap<CreateAttendance, Attendance>();
        }
    }
}
