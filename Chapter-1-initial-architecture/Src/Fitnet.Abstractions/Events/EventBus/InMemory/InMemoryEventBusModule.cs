namespace EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus.InMemory;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

internal static class InMemoryEventBusModule
{
    internal static IServiceCollection AddInMemoryEventBus(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IEventBus, InMemoryEventBus>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
