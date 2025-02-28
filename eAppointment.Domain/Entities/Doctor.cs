using eAppointment.Domain.Enums;

namespace eAppointment.Domain.Entities;
public sealed class Doctor
{
    public Doctor()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public DepartmentEnum DepartmentEnum { get; set; } = DepartmentEnum.Acil;
}
