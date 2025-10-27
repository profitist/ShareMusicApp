namespace Wavy.Domain.Sharing;

public record Reaction(Guid SenderId, Guid ReceiverId, string Emoji);