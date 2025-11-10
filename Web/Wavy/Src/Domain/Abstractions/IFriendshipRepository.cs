namespace Wavy.Domain.Abstractions;
using Wavy.Domain.Users;

public interface IFriendshipRepository
{
    Task<Friendship?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Friendship?> FindByUsersAsync(Guid userId1, Guid userId2, CancellationToken cancellationToken = default);
    Task<IEnumerable<Friendship>> GetPendingRequestsForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Friendship>> GetAcceptedFriendshipsForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Friendship friendship, CancellationToken cancellationToken = default);
    void Update(Friendship friendship);
    void Remove(Friendship friendship);
}