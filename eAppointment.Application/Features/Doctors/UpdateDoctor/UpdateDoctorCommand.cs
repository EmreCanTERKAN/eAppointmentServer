using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Doctors.UpdateDoctor;

public sealed record UpdateDoctorCommand(
    Guid Id,
    string FirstName,
    string LastName,
    int DepartmentValue) : IRequest<Result<string>>;
