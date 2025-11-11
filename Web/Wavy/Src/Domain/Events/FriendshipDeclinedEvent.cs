using Wavy.Domain.Core;

namespace Wavy.Domain.Events;

public record FriendshipDeclinedEvent(Guid FriendshipId, Guid RequesterId, Guid AddresseeId) : IDomainEvent;