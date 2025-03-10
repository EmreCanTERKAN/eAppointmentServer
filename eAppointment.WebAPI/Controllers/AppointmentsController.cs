﻿using eAppointment.Application.Features.Appointments.CreateAppointment;
using eAppointment.Application.Features.Appointments.DeleteAppointmentById;
using eAppointment.Application.Features.Appointments.GetAllAppointments;
using eAppointment.Application.Features.Appointments.GetAllDoctorsByDepartment;
using eAppointment.Application.Features.Appointments.GetPatientByIdentityNumber;
using eAppointment.Application.Features.Appointments.UpdateAppointment;
using eAppointment.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.WebAPI.Controllers;

public sealed class AppointmentsController : ApiController
{
    public AppointmentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAllDoctorsByDepartment(GetAllDoctorsByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetAllByDoctorId(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetPatientByIdentityNumber(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
