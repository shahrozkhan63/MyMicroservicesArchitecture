using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
