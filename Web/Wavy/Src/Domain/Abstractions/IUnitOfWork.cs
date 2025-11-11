namespace Wavy.Domain.Abstractions;

/// <summary>
/// Паттерн проектирования из книжки, представляющий одну бизнес-транзакцию, Атом во всем процессе 
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}