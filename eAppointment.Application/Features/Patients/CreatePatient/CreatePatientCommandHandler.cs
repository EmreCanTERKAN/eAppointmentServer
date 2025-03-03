using AutoMapper;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.CreatePatient;

internal sealed class CreatePatientCommandHandler(
    IUnitOfWork unitOfWork,
    IPatientRepository patientRepository,
    IMapper mapper) : IRequestHandler<CreatePatientCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        if (patientRepository.Any(p => p.IdentityNumber == request.IdentityNumber))
            return Result<string>.Failure("Patient identity already use..");

        Patient patient = mapper.Map<Patient>(request);

        await patientRepository.AddAsync(patient, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Patient create is successfull";
    }
}
