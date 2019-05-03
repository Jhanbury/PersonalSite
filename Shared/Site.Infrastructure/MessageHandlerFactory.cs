using System;
using Autofac;
using Site.Application.Messaging;
using Site.Infrastructure.Messages;

namespace Site.Infrastructure
{
    public class MessageHandlerFactory : IMessageHandlerFactory
    {
        private readonly ILifetimeScope _container;

        public MessageHandlerFactory(ILifetimeScope container)
        {
            _container = container;
        }

        public IMessageHandler<IMessage> ResolveMessageHandler(IMessage message)
        {
            var service = _container.ResolveKeyed<IMessageHandler<IMessage>>(message.Type);
            return service ?? throw new Exception("No service found for Message Type");
        }
    }
}