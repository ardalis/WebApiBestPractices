using System.Linq.Expressions;

namespace BackendData;

/// <summary>
/// Source: My reference app https://github.com/dotnet-architecture/eShopOnWeb
/// Consider adding Ardalis.Specification if you implement this
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAsyncRepository<T> where T : BaseEntity
{
  Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);

  Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken);

  Task<IReadOnlyList<T>> ListAllAsync(int perPage, int page, CancellationToken cancellationToken);

	Task<IReadOnlyList<T>> ListByExpressionAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

	Task<T> AddAsync(T entity, CancellationToken cancellationToken);

  Task UpdateAsync(T entity, CancellationToken cancellationToken);

  Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
