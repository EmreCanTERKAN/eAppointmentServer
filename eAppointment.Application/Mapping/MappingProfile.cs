using AutoMapper;
using eAppointment.Application.Features.Doctors.CreateDoctor;
using eAppointment.Application.Features.Doctors.UpdateDoctor;
using eAppointment.Application.Features.Patients.CreatePatient;
using eAppointment.Application.Features.Patients.UpdatePatient;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Enums;

namespace eAppointment.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateDoctorCommand, Doctor>().ForMember(member => member.DepartmentEnum,options =>
        {
            options.MapFrom(map => DepartmentEnum.FromValue(map.DepartmentValue));
        });
        CreateMap<UpdateDoctorCommand, Doctor>().ForMember(member => member.DepartmentEnum,options =>
        {
            options.MapFrom(map => DepartmentEnum.FromValue(map.DepartmentValue));
        });

        CreateMap<CreatePatientCommand, Patient>();
        CreateMap<UpdatePatientCommand, Patient>();
    }
}
