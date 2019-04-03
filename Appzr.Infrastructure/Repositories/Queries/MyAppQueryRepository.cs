using Appzr.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Appzr.Infrastructure.Repositories.Queries
{
    public sealed class MyAppQueryRepository : QueryRepository<MyAppEntity>
    {
        public MyAppQueryRepository(IConfiguration configuration) : base(configuration, "MyApps")
        {
        }
    }
}
