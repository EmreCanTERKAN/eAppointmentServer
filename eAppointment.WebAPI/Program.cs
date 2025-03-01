using eAppointment.Application;
using eAppointment.Domain.Entities;
using eAppointment.Infrastructure;
using eAppointment.WebAPI;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7038") 
            .AllowAnyHeader()  // Tüm header'lara izin ver
            .AllowAnyMethod()  // Tüm HTTP metodlarýna izin ver
            .AllowCredentials();  // Kimlik doðrulama bilgilerini kabul et
    });
});



builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Helper.CreateUserAsync(app).Wait();

app.Run();
