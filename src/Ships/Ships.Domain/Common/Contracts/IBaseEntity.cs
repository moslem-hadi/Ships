using System.ComponentModel.DataAnnotations.Schema;

namespace Ships.Domain.Common.Contracts;
public interface IBaseEntity
{
    [NotMapped]
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }
    void AddDomainEvent(BaseEvent domainEvent);
    void RemoveDomainEvent(BaseEvent domainEvent);
    void ClearDomainEvents();
}