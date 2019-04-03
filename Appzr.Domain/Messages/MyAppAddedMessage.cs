using System;

namespace Appzr.Domain.Messages
{
    [Serializable]
    public sealed class MyAppAddedMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
