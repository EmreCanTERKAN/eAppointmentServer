using eAppointment.Application.Features.Users.GetAllUser;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(
UserManager<AppUser> userManager,
RoleManager<AppRole> roleManager,
IUserRoleRepository userRoleRepository
    ) : IRequestHandler<GetAllUserQuery, Result<List<GetAllUserQueryResponse>>>
{
    public async Task<Result<List<GetAllUserQueryResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        List<AppUser> users = await userManager.Users.OrderBy(p => p.FirstName).ToListAsync(cancellationToken);

        List<GetAllUserQueryResponse> response =
            users.Select(s => new GetAllUserQueryResponse()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.FullName,
                UserName = s.UserName,
                Email = s.Email
            }).ToList();

        foreach (var item in response)
        {
            List<AppUserRole> userRoles = await userRoleRepository.Where(p => p.UserId == item.Id).ToListAsync(cancellationToken);

            List<Guid> stringRoles = new();
            List<string?> stringRoleNames = new();

            foreach (var userRole in userRoles)
            {
                AppRole? role =
                    await roleManager
                    .Roles
                    .Where(p => p.Id == userRole.RoleId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (role is not null)
                {
                    stringRoles.Add(role.Id);
                    stringRoleNames.Add(role.Name);
                }
            }

            item.RoleIds = stringRoles;
            item.RoleNames = stringRoleNames;
        }

        return response;
    }
}