namespace Wavy.Domain.Users;

public class Friendship
{
    public Guid Friend1Id { get; private set; }
    public Guid Friend2Id { get; private set; }
    public FriendshipStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ModifiedAt { get; private set; }

    public Friendship(Guid friend1Id, Guid friend2Id)
    {
        Friend1Id = friend1Id;
        Friend2Id = friend2Id;
        Status = FriendshipStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        Status = FriendshipStatus.Accepted;
        ModifiedAt = DateTime.UtcNow;
        //??
    }

    public void Decline()
    {
        Status = FriendshipStatus.Declined;
        ModifiedAt = DateTime.UtcNow;
        //??
    }
}