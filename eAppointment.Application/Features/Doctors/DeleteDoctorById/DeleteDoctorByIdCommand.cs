using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Doctors.DeleteDoctor;

public sealed record DeleteDoctorByIdCommand(
    Guid Id) : IRequest<Result<string>>;
