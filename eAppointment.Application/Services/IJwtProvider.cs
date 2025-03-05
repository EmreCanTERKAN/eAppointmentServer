using eAppointment.Domain.Entities;

namespace eAppointment.Application.Services;
public interface IJwtProvider
{
    Task<string> CreateTokenAsync(AppUser appUser, CancellationToken cancellationToken);
}
