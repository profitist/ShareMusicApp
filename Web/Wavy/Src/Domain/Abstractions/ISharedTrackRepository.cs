using Wavy.Domain.Sharing;

namespace Wavy.Domain.Abstractions;

public interface ISharedTrackRepository
{
    Task<SharedTrack?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SharedTrack>> GetForReceiverAsync(Guid receiverId, CancellationToken cancellationToken = default);
    Task AddAsync(SharedTrack sharedTrack, CancellationToken cancellationToken = default);
    void Update(SharedTrack sharedTrack);
}