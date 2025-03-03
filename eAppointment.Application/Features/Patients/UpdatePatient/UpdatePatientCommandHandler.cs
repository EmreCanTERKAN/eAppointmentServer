using AutoMapper;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.UpdatePatient;

public sealed class UpdatePatientCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdatePatientCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        Patient? patient = await patientRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (patient is null)
            return Result<string>.Failure("Patient Not Found");

        if(patient.IdentityNumber != request.IdentityNumber)
        {
            if (patientRepository.Any(p => p.IdentityNumber == request.IdentityNumber))
                return Result<string>.Failure("Identity number is already in use by another patient.");
        }

        mapper.Map(request, patient);

        patientRepository.Update(patient);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Patient update is successfull";
    }
}
