using eAppointment.Application;
using eAppointment.Infrastructure;
using eAppointment.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
            .AllowAnyHeader()  // T�m header'lara izin ver
            .AllowAnyMethod()  // T�m HTTP metodlar�na izin ver
            .AllowCredentials();  // Kimlik do�rulama bilgilerini kabul et
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
