using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Users.GetAllUser;

public sealed record GetAllUserQuery() : IRequest<Result<List<GetAllUserQueryResponse>>>;
