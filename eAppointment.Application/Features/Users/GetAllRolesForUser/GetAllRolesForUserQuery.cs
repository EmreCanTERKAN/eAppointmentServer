using eAppointment.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Users.GetAllRolesForUser;

public sealed record GetAllRolesForUserQuery() : IRequest<Result<List<AppRole>>>;
