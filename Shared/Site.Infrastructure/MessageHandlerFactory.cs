using System;
using Site.Application.Interfaces.Messaging;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Modules;

namespace Site.Infrastructure
{
  public class MessageHandlerFactory : IMessageHandlerFactory
    {
        
        private readonly HandlerResolver _resolver;
        public MessageHandlerFactory(HandlerResolver resolver)
        {
            _resolver = resolver;
        }

        public IMessageHandler<IMessage> ResolveMessageHandler(IMessage message)
        {
          return _resolver(message.Type);
        }
    }
}
