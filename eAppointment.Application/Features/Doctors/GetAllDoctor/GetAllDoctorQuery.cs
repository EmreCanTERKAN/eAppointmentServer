using eAppointment.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Doctors.GetAllDoctor;

public sealed record GetAllDoctorQuery () :IRequest<Result<List<Doctor>>>;
