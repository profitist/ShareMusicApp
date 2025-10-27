namespace Wavy.Domain.Sharing;

public record Track(Guid Id, string Title, string Artist, string? AlbumCoverUrl, string SourceUrl, MusicPlatform Platform);