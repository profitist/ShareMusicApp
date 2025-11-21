namespace Wavy.Application.DTOs;

public record RegisterUserDto(string Username, string Password);

public record LoginDto(string Username, string Password);

public record UserDto(Guid Id, string Username, string? ProfilePictureUrl);

public record AuthResponseDto(string Token, UserDto User);

public record FriendshipDto(Guid FriendshipId, Guid FriendId, string Username, string Status);