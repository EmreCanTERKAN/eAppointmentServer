using AutoMapper;
using eAppointment.Application.Features.Doctors.CreateDoctor;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Enums;

namespace eAppointment.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDoctorCommand, Doctor>().ForMember(member => member.DepartmentEnum,options =>
        {
            options.MapFrom(map => DepartmentEnum.FromValue(map.DepartmentEnum));
        });
    }
}
