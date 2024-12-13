using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Blog.SharedKernel;

public interface IReadRepository<TEntity> where TEntity : class, IAggregateRoot
{
    IQueryable<TEntity> GetByCondition(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);
    Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    TEntity? GetById(object id);
    Task<PagedResult<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
        int pageIndex = 1,
        int pageSize = 10,
        bool tracking = false,
        CancellationToken cancellationToken = default);
    PagedResult<TEntity> GetAll(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
        int pageIndex = 1,
        int pageSize = 10,
        bool tracking = false);
}