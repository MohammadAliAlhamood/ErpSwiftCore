using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Notifications.Interface
{
    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(
            TEvent @event, CancellationToken ct = default)
            where TEvent : notnull;
    }


}
