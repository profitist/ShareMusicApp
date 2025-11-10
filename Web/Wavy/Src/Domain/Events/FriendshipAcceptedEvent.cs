using Wavy.Domain.Core;

namespace Wavy.Domain.Events;

public record FriendshipAcceptedEvent(Guid FriendshipId, Guid RequesterId, Guid AddresserId) : IDomainEvent;