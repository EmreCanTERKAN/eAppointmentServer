using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Doctors.CreateDoctor;

public sealed record CreateDoctorCommand(
    string FirstName,
    string Lastname,
    int DepartmentValue) : IRequest<Result<string>>;
