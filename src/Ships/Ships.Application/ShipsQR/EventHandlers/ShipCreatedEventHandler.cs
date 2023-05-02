using MediatR;
using Microsoft.Extensions.Logging;
using Ships.Domain.Events;

namespace Ships.Application.ShipsQR.EventHandlers
{
    internal class ShipCreatedEventHandler : INotificationHandler<ShipCreatedEvent>
    {
        private readonly ILogger<ShipCreatedEventHandler> _logger;

        public ShipCreatedEventHandler(ILogger<ShipCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ShipCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Ship Created, Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
