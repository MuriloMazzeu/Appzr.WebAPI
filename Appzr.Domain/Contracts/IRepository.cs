using Appzr.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Appzr.Domain.Contracts
{
    public interface IRepository<T> where T : EntityBase
    {
        Task SaveAsync(T entity);
        Task<T> FindByIdAsync(Guid id);
    }
}
