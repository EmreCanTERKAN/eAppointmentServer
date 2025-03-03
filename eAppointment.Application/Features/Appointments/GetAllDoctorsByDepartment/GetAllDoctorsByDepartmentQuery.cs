using eAppointment.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.GetAllDoctorsByDepartment;

public sealed record GetAllDoctorsByDepartmentQuery(
    int DepartmentValue) : IRequest<Result<List<Doctor>>>;
