﻿using Microsoft.Extensions.DependencyInjection;

namespace eAppointment.Application;
public static class ApplicationRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly);
        });
        return services;
    }
}
