using System;
using System.Threading.Tasks;

namespace Appzr.Domain.Contracts
{
    public interface ICommand
    {
        Guid CommandId { get; }

        Task ExecuteAsync();
    }
}
