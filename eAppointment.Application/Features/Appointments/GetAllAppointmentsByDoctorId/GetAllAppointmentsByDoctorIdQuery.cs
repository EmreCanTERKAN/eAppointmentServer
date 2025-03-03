using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.GetAllAppointments;

public sealed record GetAllAppointmentsByDoctorIdQuery(
    Guid DoctorId) : IRequest<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>;
