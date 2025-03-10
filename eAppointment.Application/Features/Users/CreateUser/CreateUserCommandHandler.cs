﻿using AutoMapper;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Application.Features.Users.CreateUser;

internal sealed class CreateUserCommandHandler(
    UserManager<AppUser> userManager,
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await userManager.Users.AnyAsync(p => p.Email == request.Email))
            return Result<string>.Failure("Email already exits");

        if (await userManager.Users.AnyAsync(p => p.UserName == request.UserName))
            return Result<string>.Failure("User name already exits");

        AppUser user = mapper.Map<AppUser>(request);

        IdentityResult result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());



        if (request.RoleIds.Any())
        {
            List<AppUserRole> userRoles = new();
            foreach (var roleId in request.RoleIds)
            {
                AppUserRole userRole = new()
                {
                    RoleId = roleId,
                    UserId = user.Id
                };
                userRoles.Add(userRole);
            }
            await userRoleRepository.AddRangeAsync(userRoles, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return "User create is successful";
    }
}
