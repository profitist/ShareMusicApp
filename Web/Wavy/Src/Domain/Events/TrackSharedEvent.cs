using Wavy.Domain.Core;

namespace Wavy.Domain.Events;

public record TrackSharedEvent(Guid SharedTrackId, Guid SenderId, ICollection<Guid> ReceiversId) : IDomainEvent;