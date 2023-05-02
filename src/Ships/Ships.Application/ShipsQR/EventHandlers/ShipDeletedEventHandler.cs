using MediatR;
using Microsoft.Extensions.Logging;
using Ships.Domain.Events;

namespace Ships.Application.ShipsQR.EventHandlers
{
    internal class ShipDeletedEventHandler : INotificationHandler<ShipDeletedEvent>
    {
        private readonly ILogger<ShipDeletedEventHandler> _logger;

        public ShipDeletedEventHandler(ILogger<ShipDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ShipDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
