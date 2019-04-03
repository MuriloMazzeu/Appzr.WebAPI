using System;

namespace Appzr.Domain.Entities
{
    public sealed class CommandEntity : EntityBase
    {
        public DateTime ExecutedAt { get; private set; }

        public CommandEntity(Guid id) : base(id)
        {
            ExecutedAt = DateTime.Now;
        }
    }
}
