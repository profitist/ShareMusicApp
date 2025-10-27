namespace Wavy.Domain.Sharing;

public class SharedTrack
{
    public Guid Id { get; private set; }
    public Guid SenderId { get; private set; }
    
    public List<Guid> ReceiversId { get; private set; }
    public Track Track { get; private set; }
    public DateTime SentAt { get; private set; }
    public string Description { get; private set; }

    private readonly List<Reaction> reactions = new();
    public IReadOnlyCollection<Reaction> Reactions => reactions.AsReadOnly();

    public SharedTrack(Guid senderId, List<Guid> receiverId, Track track, string description)
    {
        Id = Guid.NewGuid();
        SenderId = senderId;
        ReceiversId = receiverId;
        Track = track;
        Description = description;
        SentAt = DateTime.Now;
    }
}