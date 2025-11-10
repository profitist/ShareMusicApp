using Wavy.Application.Abstractions;
using Wavy.Application.DTOs;
using Wavy.Domain.Abstractions;
using Wavy.Domain.Users;

namespace Wavy.Application.Services;

public class FriendshipService(
    IUserRepository userRepository,
    IFriendshipRepository friendshipRepository,
    IUnitOfWork unitOfWork)
    : IFriendshipService
{
    public async Task SendFriendRequestAsync(Guid requesterId, Guid addresseeId, CancellationToken cancellationToken)
    {
        if (requesterId == addresseeId)
            throw new InvalidOperationException("You cannot send a friend request to yourself");

        var requester = await userRepository.GetByIdAsync(requesterId, cancellationToken);
        var addressee = await userRepository.GetByIdAsync(addresseeId, cancellationToken);

        if (requester is null || addressee is null)
            throw new Exception("One or both users not found.");

        var existingFriendship = await friendshipRepository.FindByUsersAsync(requesterId, addresseeId, cancellationToken);
        if (existingFriendship is not null)
            throw new InvalidOperationException("A friendship request already exists or has been accepted");
        
        var friendship = new Friendship(requesterId, addresseeId);
        await friendshipRepository.AddAsync(friendship, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AcceptFriendRequestAsync(Guid friendshipId, Guid currentUserId, CancellationToken cancellationToken)
    {
        var friendship = await friendshipRepository.GetByIdAsync(friendshipId, cancellationToken);
        if (friendship is null)
            throw new Exception("Friendship request not found");
        if (friendship.AddresserId != currentUserId)
            throw new Exception("You are not authorized to accept this request.");
        friendship.Accept();
        friendshipRepository.Update(friendship); 
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeclineFriendRequestAsync(Guid friendshipId, Guid currentUserId, CancellationToken cancellationToken)
    {
        var friendship = await friendshipRepository.GetByIdAsync(friendshipId, cancellationToken);
        if (friendship is null)
            throw new Exception("Friendship request not found.");
        if (friendship.AddresserId != currentUserId)
            throw new Exception("You are not authorized to decline this request.");
        friendship.Decline();
        friendshipRepository.Update(friendship);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveFriendAsync(Guid friendshipId, Guid currentUserId, CancellationToken cancellationToken)
    {
        var friendship = await friendshipRepository.GetByIdAsync(friendshipId, cancellationToken);
        
        if (friendship is null)
            throw new Exception("Friendship not found");
        
        if (friendship.RequesterId != currentUserId && friendship.AddresserId != currentUserId)
            throw new Exception("You are not authorized to remove this friend");
        if (friendship.Status != FriendshipStatus.Accepted)
             throw new InvalidOperationException("Cannot remove a friend if the friendship is not accepted");
        
        friendshipRepository.Remove(friendship);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<IEnumerable<FriendshipDto>> GetFriendshipsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException(); //Реализация будет зависеть от того, как будем получать данные. пока хз
    }
}