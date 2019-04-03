using Appzr.Domain.Contracts;
using Appzr.Domain.Entities;
using Appzr.Domain.Messages;
using System;
using System.Threading.Tasks;

namespace Appzr.Domain.Commands
{
    public sealed class AddMyAppCommand : ICommand
    {

        public Guid CommandId { get; }

        public string Name { get; set; }
        public string Link { get; set; }

        private IRepository<MyAppEntity> MyAppRepository { get; }
        private IMessageBroker Bus { get; }

        public AddMyAppCommand(IRepository<MyAppEntity> myAppRepository, IMessageBroker bus)
        {
            Bus = bus;
            CommandId = Guid.NewGuid();
            MyAppRepository = myAppRepository;
        }

        public async Task ExecuteAsync()
        {
            var entity = new MyAppEntity();
            entity.ChangeName(Name);
            entity.ChangeLink(Link);

            await MyAppRepository.SaveAsync(entity);

            //Envia para um topico para notificar sobre as mudanças
            Bus.SendMessage(new MyAppAddedMessage()
            {
                Id = entity.Id,
                Link = entity.Link,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            });
        }
    }
}
