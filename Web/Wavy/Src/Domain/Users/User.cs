using Wavy.Domain.Core;

namespace Wavy.Domain.Users;

public class User : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string PhoneNumber { get;  private set; }
    public string Email { get; private set; }
    
    public User(Guid id, string username, string phoneNumber, string email)
    {
        if(string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException("Username cannot be empty", nameof(username));
        Id = id;
        Username = username;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public void UpdateProfile(string newUsername, string? newProfilePictureUrl, string newPhoneNumber, string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newUsername))
            throw new ArgumentNullException("Username cannot be empty", nameof(newUsername));
        Username = newUsername;
        PhoneNumber = newPhoneNumber;
        ProfilePictureUrl = newProfilePictureUrl;
        Email = newEmail;
    }
}