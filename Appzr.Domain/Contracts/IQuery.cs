using System.Threading.Tasks;

namespace Appzr.Domain.Contracts
{
    public interface IQuery<TResult>
    {
        Task<TResult> ExecuteAsync();
    }
}
