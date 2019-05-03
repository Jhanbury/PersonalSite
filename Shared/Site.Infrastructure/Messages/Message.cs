using Site.Application.Messaging;

namespace Site.Infrastructure.Messages
{
    public abstract class Message : IMessage
    {
        protected Message(string messageName, MessageType type)
        {
            MessageName = messageName;
            Type = type;
        }

        public MessageType Type { get; }
        public string MessageName { get; }
    }

    
}