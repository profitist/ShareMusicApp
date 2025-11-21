using Wavy.Application.Abstractions;
using Wavy.Application.DTOs;
using Wavy.Domain.Abstractions;
using Wavy.Domain.Users;

namespace Wavy.Application.Services;

public interface IUserService
{
    Task RegisterAsync(RegisterUserDto dto, CancellationToken ct = default);
    Task<UserDto> GetProfileAsync(Guid userId, CancellationToken ct = default);
    Task<IEnumerable<UserDto>> SearchUsersAsync(string query, CancellationToken ct = default);
}

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPasswordHasher passwordHasher;

    public UserService(
        IUserRepository repos, 
        IUnitOfWork unit, 
        IPasswordHasher hasher)
    {
        userRepository = repos;
        unitOfWork = unit;
        passwordHasher = hasher;
    }

    public async Task RegisterAsync(RegisterUserDto dto, CancellationToken ct)
    {
        var existingUser = await userRepository.GetByUsernameAsync(dto.Username, ct);
        if (existingUser is not null)
            throw new Exception("Username is already taken.");
        var passwordHash = passwordHasher.Hash(dto.Password);
        var newUser = new User(Guid.NewGuid(), dto.Username, passwordHash);
        await userRepository.AddAsync(newUser, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<UserDto> GetProfileAsync(Guid userId, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(userId, ct);
        if (user is null) 
            throw new Exception("User not found");
        return new UserDto(user.Id, user.Username, user.ProfilePictureUrl);
    }

    public async Task<IEnumerable<UserDto>> SearchUsersAsync(string query, CancellationToken ct)
    {
        var users = await userRepository.SearchAsync(query, ct);
        return users.Select(u => new UserDto(u.Id, u.Username, u.ProfilePictureUrl));
    }
}