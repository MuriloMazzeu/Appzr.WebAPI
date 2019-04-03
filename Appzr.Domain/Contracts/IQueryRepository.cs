using Appzr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Appzr.Domain.Contracts
{
    public interface IQueryRepository<T> where T : EntityBase
    {
        Task<T> FindByIdAsync(Guid id);
        Task SynchronizeDataAsync(T entity);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        
    }
}
