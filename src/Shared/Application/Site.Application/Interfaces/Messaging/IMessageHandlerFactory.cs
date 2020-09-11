namespace Site.Application.Interfaces.Messaging
{
    public interface IMessageHandlerFactory
    {
        IMessageHandler<IMessage> ResolveMessageHandler(IMessage messageType);
    }
}