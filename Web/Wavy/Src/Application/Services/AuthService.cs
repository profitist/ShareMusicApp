using System.Security.Authentication;
using Wavy.Application.DTOs;
using Wavy.Application.Abstractions;
using Wavy.Domain.Abstractions;

namespace Wavy.Application.Services;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default);
}

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;
    private readonly IJwtTokenGenerator tokenGenerator;

    public AuthService(
        IUserRepository userRepos, 
        IPasswordHasher hasher, 
        IJwtTokenGenerator generator)
    {
        userRepository = userRepos;
        passwordHasher = hasher;
        tokenGenerator = generator;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken ct)
    {
        var user = await userRepository.GetByUsernameAsync(dto.Username, ct);
        
        if (user is null)
            throw new InvalidCredentialException("Invalid credentials"); 
        if (!passwordHasher.Verify(dto.Password, user.PasswordHash))
            throw new InvalidCredentialException("Invalid credentials");
        var token = tokenGenerator.GenerateToken(user);
        return new AuthResponseDto(
            Token: token,
            User: new UserDto(user.Id, user.Username, user.ProfilePictureUrl)
        );
    }
}