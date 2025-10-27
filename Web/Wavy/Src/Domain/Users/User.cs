namespace Wavy.Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string PhoneNumber { get;  private set; }
    public string Email { get; private set; }
    
    public User(Guid id, string username, string phoneNumver, string email)
    {
        Id = id;
        Username = username;
        PhoneNumber = phoneNumver;
        Email = email;
    }

    public Friendship SendFriendRequest(Guid friendId)
    {
        throw new NotImplementedException();
    }

    public void AcceptFriendRequest(Friendship friendshipRequest)
    {
        throw new NotImplementedException();
    }

    public void DeclineFriendRequest(Friendship friendshipRequest)
    {
        throw new NotImplementedException();
    }

    public void RemoveFriend(Guid friendId)
    {
        throw new NotImplementedException();
    }
}