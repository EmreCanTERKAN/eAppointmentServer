using eAppointment.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.GetPatientByIdentityNumber;

public sealed record GetPatientByIdentityNumberQuery(
    string IdentityNumber) : IRequest<Result<Patient>>;
