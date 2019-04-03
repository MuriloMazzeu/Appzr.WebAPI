using Appzr.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Appzr.Infrastructure.Repositories
{
    public sealed class CommandRepository : MainRepository<CommandEntity>
    {
        public CommandRepository(IConfiguration configuration) : base(configuration, "Commands")
        {
            
        }
    }
}
