using Wavy.Domain.Core;
using Wavy.Domain.Events;

namespace Wavy.Domain.Sharing;

public class SharedTrack : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid SenderId { get; private set; }
    
    public List<Guid> ReceiversId { get; private set; }
    public Track Track { get; private set; }
    public DateTime SentAt { get; private set; }
    public string Description { get; private set; }

    private readonly List<Reaction> reactions = new();
    public IReadOnlyCollection<Reaction> Reactions => reactions.AsReadOnly();

    public SharedTrack(Guid senderId, List<Guid> receiversId, Track track, string description)
    {
        if (receiversId is null || receiversId.Count == 0)
            throw new ArgumentException("There must be at least one receiver");
        if (receiversId.Contains(senderId))
            throw new InvalidOperationException("Sender cannot be the receiver");
        Id = Guid.NewGuid();
        SenderId = senderId;
        ReceiversId = receiversId;
        Track = track;
        Description = description;
        SentAt = DateTime.Now;
        
        AddDomainEvent(new TrackSharedEvent(Id, SenderId, ReceiversId));
    }
    
    public void AddReaction(Reaction reaction)
    {
        if (reactions.Any(r => r.SenderId == reaction.SenderId))
            return; 
        reactions.Add(reaction);
    }
}