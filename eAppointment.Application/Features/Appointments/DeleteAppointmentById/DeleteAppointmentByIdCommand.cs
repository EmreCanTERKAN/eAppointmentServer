using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.DeleteAppointmentById;

public sealed record DeleteAppointmentByIdCommand(
    Guid Id) : IRequest<Result<string>>;
