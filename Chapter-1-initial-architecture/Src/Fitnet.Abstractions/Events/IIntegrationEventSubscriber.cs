namespace EvolutionaryArchitecture.Fitnet.Abstractions.Events;

using MediatR;

public interface IIntegrationEventSubscriber<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent;
