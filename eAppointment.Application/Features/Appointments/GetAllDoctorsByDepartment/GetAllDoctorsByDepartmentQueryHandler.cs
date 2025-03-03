using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.GetAllDoctorsByDepartment;

internal sealed class GetAllDoctorsByDepartmentQueryHandler(
    IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorsByDepartmentQuery, Result<List<Doctor>>>
{
    public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsByDepartmentQuery request, CancellationToken cancellationToken)
    {
        List<Doctor> doctors = await doctorRepository
            .Where(x => x.DepartmentEnum == request.DepartmentValue)
            .OrderBy(p => p.FirstName)
            .ToListAsync(cancellationToken);
        return doctors;
    }
}
