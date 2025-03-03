using eAppointment.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.GetAllPatient;

public sealed record GetAllPatientQuery() : IRequest<Result<List<Patient>>>;
