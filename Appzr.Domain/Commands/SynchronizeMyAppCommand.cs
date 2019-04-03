using Appzr.Domain.Contracts;
using Appzr.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Appzr.Domain.Commands
{
    public sealed class SynchronizeMyAppCommand : ICommand
    {

        public Guid CommandId { get; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }

        private IQueryRepository<MyAppEntity> MyAppRepository { get; }

        public SynchronizeMyAppCommand(IQueryRepository<MyAppEntity> myAppRepository)
        {
            CommandId = Guid.NewGuid();
            MyAppRepository = myAppRepository;
        }

        public async Task ExecuteAsync()
        {
            var entity = new MyAppEntity(Id, Name, Link, CreatedAt);
            await MyAppRepository.SynchronizeDataAsync(entity);
        }
    }
}
