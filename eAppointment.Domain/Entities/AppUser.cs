using Microsoft.AspNetCore.Identity;

namespace eAppointment.Domain.Entities;
public sealed class AppUser  : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
}
