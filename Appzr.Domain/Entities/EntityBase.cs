using System;

namespace Appzr.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; }

        public EntityBase(Guid id)
        {
            Id = id;
        }
    }
}
