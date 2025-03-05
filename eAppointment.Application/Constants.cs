using eAppointment.Domain.Entities;

namespace eAppointment.Application;

public static class Constants
{
    public static List<AppRole> GetRoles()
    {
        List<string> roles = new()
        {
            "Admin",
            "Doctor",
            "Personel"
        };
        return roles.Select(s => new AppRole() { Name = s }).ToList();
    }

}



