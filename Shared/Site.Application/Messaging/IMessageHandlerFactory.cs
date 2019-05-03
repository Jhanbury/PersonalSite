namespace Site.Application.Messaging
{
    public interface IMessageHandlerFactory
    {
        IMessageHandler<IMessage> ResolveMessageHandler(IMessage messageType);
    }
}