using Wavy.Domain.Core;
using Wavy.Domain.Events;


namespace Wavy.Domain.Users;

public class Friendship : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid RequesterId { get; private set; }
    public Guid AddresserId { get; private set; }
    public FriendshipStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }

    public Friendship(Guid requesterId, Guid addresserId)
    {
        Id = Guid.NewGuid();
        RequesterId = requesterId;
        AddresserId = addresserId;
        Status = FriendshipStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        if(Status != FriendshipStatus.Pending)
            throw new InvalidOperationException("Cannot accept a friendship that is not pending");
        Status = FriendshipStatus.Accepted;
        ModifiedAt = DateTime.UtcNow;
        AddDomainEvent(new FriendshipAcceptedEvent(Id, RequesterId, AddresserId));
    }

    public void Decline()
    {
        if (Status != FriendshipStatus.Pending)
            throw new InvalidOperationException("Cannot decline a friendship that is not in pending");
        Status = FriendshipStatus.Declined;
        ModifiedAt = DateTime.UtcNow;
        AddDomainEvent(new FriendshipDeclinedEvent(Id, RequesterId, AddresserId));
    }
}