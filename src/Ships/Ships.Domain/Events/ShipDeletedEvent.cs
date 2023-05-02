namespace Ships.Domain.Events;

public record ShipDeletedEvent(Ship ship) : BaseEvent
{
}
