namespace Ships.Domain.Events;

public record ShipCreatedEvent(Ship ship) : BaseEvent
{
}
