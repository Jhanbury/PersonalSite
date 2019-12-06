using System;
using Autofac;
using Site.Application.Interfaces.Messaging;
using Site.Infrastructure.MessageHandlers;
using Site.Infrastructure.Messages;

namespace Site.Infrastructure
{
    public class MessageHandlerFactory : IMessageHandlerFactory
    {
        private readonly IMessageHandler<IMessage> _githubMessageHandler;

        public MessageHandlerFactory(GithubMessageHandler handler)
        {
            _githubMessageHandler = handler;
        }

        public IMessageHandler<IMessage> ResolveMessageHandler(IMessage message)
        {
            IMessageHandler<IMessage> service = null;
            switch (message.Type)
            {
                case MessageType.GithubRepoUpdate:
                    return _githubMessageHandler;
            }
            return service ?? throw new Exception("No service found for Message Type");
        }
    }
}