using Appzr.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Appzr.Infrastructure.Repositories
{
    public sealed class MyAppRepository : MainRepository<MyAppEntity>
    {
        public MyAppRepository(IConfiguration configuration) : base(configuration, "MyApps")
        {
            
        }
    }
}
