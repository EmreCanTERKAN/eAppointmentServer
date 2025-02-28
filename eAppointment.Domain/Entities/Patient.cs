namespace eAppointment.Domain.Entities;
public sealed class Patient
{
    public Patient()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Town { get; set; } = default!;
    public string FullAddress { get; set; } = default!;

}
