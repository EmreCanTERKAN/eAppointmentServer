using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.CreateAppointment;

internal sealed class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    IPatientRepository patientRepository) : IRequestHandler<CreateAppointmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {

        Patient patient = await patientRepository.GetByExpressionAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);
        if (patient is null)
        {
            patient = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentityNumber,
                City = request.City,
                Town = request.Town,
                FullAddress = request.FullAddress
            };

            await patientRepository.AddAsync(patient, cancellationToken);
        }

        Appointment appointment = new()
        {
            DoctorId = request.DoctorId,
            PatientId = !string
            .IsNullOrEmpty(request.PatientId) ? Guid.Parse(request.PatientId) : patient.Id,
            StartDate = Convert.ToDateTime(request.StartDate),
            EndDate = Convert.ToDateTime(request.EndDate),
            IsCompleted = false,
        };

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Appointment create is successfull";


    }
}
