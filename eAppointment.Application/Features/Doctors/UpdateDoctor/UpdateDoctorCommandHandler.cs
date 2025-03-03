using AutoMapper;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Doctors.UpdateDoctor;

internal sealed class UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateDoctorCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (doctor is null)
            return Result<string>.Failure("Doctor kaydı bulunamadı");
        mapper.Map(request, doctor);

        doctorRepository.Update(doctor);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Succeed("Doctor başarıyla kaydedildi.");
    }
}
