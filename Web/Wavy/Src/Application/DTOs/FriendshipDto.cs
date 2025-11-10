namespace Wavy.Application.DTOs;

public record FriendshipDto(Guid FriendshipId, Guid FriendId, string Username, string Status);