using Appzr.Domain.Contracts;
using Appzr.Domain.Entities;
using System.Threading.Tasks;

namespace Appzr.Domain.Helpers
{
    public class CqrsHelper
    {
        public CqrsHelper(IRepository<CommandEntity> commandRepository)
        {
            CommandRepository = commandRepository;
        }

        private IRepository<CommandEntity> CommandRepository { get; }

        private async Task<bool> CheckIfCommandWasExecutedAsync(ICommand command)
        {
            var entity = await CommandRepository.FindByIdAsync(command.CommandId);
            return entity != null;
        }

        private async Task MarkCommandAsExecutedAsync(ICommand command)
        {
            var entity = new CommandEntity(command.CommandId);
            await CommandRepository.SaveAsync(entity);
        }

        public async Task RunAsync<T>(T command) where T : ICommand
        {
            var commandExecuted = await CheckIfCommandWasExecutedAsync(command);
            if (!commandExecuted)
            {
                await command.ExecuteAsync();
                await MarkCommandAsExecutedAsync(command);
            }
        }

        public async Task<TResult> RunAsync<T, TResult>(T query) where T : IQuery<TResult> => 
            await query.ExecuteAsync();
    }
}
