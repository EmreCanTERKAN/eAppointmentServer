using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Application.Features.Doctors.GetAllDoctor;

internal sealed class GetAllDoctorQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorQuery, Result<List<Doctor>>>
{
    public async Task<Result<List<Doctor>>> Handle(GetAllDoctorQuery request, CancellationToken cancellationToken)
    {
       List<Doctor> doctors = await doctorRepository.GetAll().OrderBy(p => p.DepartmentEnum).ThenBy(p => p.FirstName).ToListAsync(cancellationToken);
        return doctors;
    }
}
