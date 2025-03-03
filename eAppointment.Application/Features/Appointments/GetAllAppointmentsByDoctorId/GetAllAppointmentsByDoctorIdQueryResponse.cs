using eAppointment.Domain.Entities;

namespace eAppointment.Application.Features.Appointments.GetAllAppointments;

public sealed record GetAllAppointmentsByDoctorIdQueryResponse(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    string Text,
    Patient Patient);
