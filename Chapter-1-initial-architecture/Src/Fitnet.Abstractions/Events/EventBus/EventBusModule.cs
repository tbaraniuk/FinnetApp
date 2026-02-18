namespace EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using EvolutionaryArchitecture.Fitnet.Abstractions.Events.EventBus.InMemory;

public static class EventBusModule
{
    public static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
}
