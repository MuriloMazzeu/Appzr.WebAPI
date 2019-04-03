using System;
using System.Threading.Tasks;

namespace Appzr.Domain.Contracts
{
    public interface IMessageBroker
    {
        void SendMessage<T>(T message);
        void ReceiveMessage(Func<object, Task> callback);
    }
}
