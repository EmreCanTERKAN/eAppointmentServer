using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.CreateAppointment;

public sealed record CreateAppointmentCommand(
    string StartDate,
    string EndDate,
    Guid DoctorId,
    string PatientId,
    string FirstName,
    string LastName,
    string IdentityNumber,
    string City,
    string Town,
    string FullAddress) : IRequest<Result<string>>;
