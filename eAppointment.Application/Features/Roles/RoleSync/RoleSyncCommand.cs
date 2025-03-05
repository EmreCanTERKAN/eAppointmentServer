using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Roles.RoleSync;

public sealed record RoleSyncCommand() : IRequest<Result<string>>;
