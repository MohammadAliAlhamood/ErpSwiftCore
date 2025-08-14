using ErpSwiftCore.Notifications.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Notifications
{
    // 1) إنشئ كلاس ينفّذ IEventDispatcher:
    public class MediatRDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;
        public MediatRDispatcher(IMediator mediator)
            => _mediator = mediator;

        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default)
            where TEvent : notnull
            => _mediator.Publish(@event, ct);
    }

}
