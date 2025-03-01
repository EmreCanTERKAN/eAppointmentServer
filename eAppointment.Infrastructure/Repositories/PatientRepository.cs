using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using eAppointment.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Infrastructure.Repositories;

internal sealed class PatientRepository : Repository<Patient, ApplicationDbContext>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext context) : base(context)
    {
    }
}
