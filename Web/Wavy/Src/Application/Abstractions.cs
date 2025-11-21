using Wavy.Application.DTOs;

namespace Wavy.Application.Abstractions;

public interface IFriendshipService
{
    Task SendFriendRequestAsync(Guid requesterId, Guid addresseeId, CancellationToken cancellationToken = default);
    Task AcceptFriendRequestAsync(Guid friendshipId, Guid currentUserId, CancellationToken cancellationToken = default);
    Task DeclineFriendRequestAsync(Guid friendshipId, Guid currentUserId, CancellationToken cancellationToken = default);
    Task RemoveFriendAsync(Guid friendshipId, Guid currentUserId, CancellationToken cancellationToken = default);
    Task<IEnumerable<FriendshipDto>> GetFriendshipsAsync(Guid userId, CancellationToken cancellationToken = default);
}

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashedPassword);
}

public interface IJwtTokenGenerator
{
    string GenerateToken(Domain.Users.User user);
}