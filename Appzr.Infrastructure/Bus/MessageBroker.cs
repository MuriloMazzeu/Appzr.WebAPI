using Appzr.Domain.Contracts;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Appzr.Infrastructure.Bus
{
    public sealed class MessageBroker : IMessageBroker
    {
        public MessageBroker()
        {
            topic = "events";
            PubSocket = new PublisherSocket();
            PubSocket.Options.SendHighWatermark = 1000;
            PubSocket.Bind("tcp://127.0.0.1:12345");

            SubSocket = new SubscriberSocket();
            SubSocket.Options.ReceiveHighWatermark = 1000;
            SubSocket.Connect("tcp://127.0.0.1:12345");
            SubSocket.Subscribe(topic);
        }

        private readonly string topic;

        private byte[] Serialize<T>(T data)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, data);
                return ms.ToArray();
            }
        }

        private object Deserialize(byte[] data)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(ms);
            }
        }

        public void SendMessage<T>(T message)
        {
            var data = Serialize(message);
            PubSocket.SendMoreFrame(topic).SendFrame(data);
        }

        public void ReceiveMessage(Func<object, Task> callback)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var messageTopicReceived = SubSocket.ReceiveFrameString();
                    var data = SubSocket.ReceiveFrameBytes();
                    var message = Deserialize(data);
                    await callback(message);
                }
            });
        }

        private PublisherSocket PubSocket { get; }
        private SubscriberSocket SubSocket { get; }
    }
}
