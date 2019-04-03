using Appzr.Domain.Contracts;
using Appzr.Domain.Entities;
using Appzr.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appzr.Domain.Queries
{
    public sealed class ListMyAppsQuery : IQuery<IEnumerable<MyAppVM>>
    {
        private IQueryRepository<MyAppEntity> MyAppRepository { get; }

        public ListMyAppsQuery(IQueryRepository<MyAppEntity> myAppRepository)
        {
            MyAppRepository = myAppRepository;
        }

        public async Task<IEnumerable<MyAppVM>> ExecuteAsync()
        {            
            var entities = await MyAppRepository.FindByAsync(e => e.Link != null);
            var result = new List<MyAppVM>(entities?.Count() ?? 0);

            if(entities != null)
            {
                foreach (var entity in entities)
                {
                    result.Add(new MyAppVM()
                    {
                        Link = entity.Link,
                        Name = entity.Name
                    });
                }
            }

            return result;
        }
    }
}
