using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.DeletePatientById;

public sealed class DeletePatientByIdHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeletePatientByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
    {
        Patient? patient = await patientRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
        if (patient is null)
            return Result<string>.Failure("Patient Not Found");
        patientRepository.Delete(patient);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Succeed("Patient is deleted");

    }
}
