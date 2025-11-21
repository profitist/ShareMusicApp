using Wavy.Domain.Core;

namespace Wavy.Domain.Users;

public class User : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string PhoneNumber { get;  private set; }
    public string Email { get; private set; }
    
    public string PasswordHash { get; private set; }
    
    public User(Guid id, string username, string phoneNumber, string email, string passwordHash)
    {
        if(string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException("Username cannot be empty", nameof(username));
        if(string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentNullException("Password cannot be empty", nameof(passwordHash));
        Id = id;
        Username = username;
        PhoneNumber = phoneNumber;
        Email = email;
        PasswordHash = passwordHash;
    }

    public User(Guid id, string username, string passwordHash)
    {
        if(string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException("Username cannot be empty", nameof(username));
        if(string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentNullException("Password cannot be empty", nameof(passwordHash));
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
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