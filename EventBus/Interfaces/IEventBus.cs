using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Interfaces
{
    public interface IEventBus
    {
        void Publish(string eventName, object data);
        void Subscribe(string eventName, Action<object> eventHandler);
    }
}
