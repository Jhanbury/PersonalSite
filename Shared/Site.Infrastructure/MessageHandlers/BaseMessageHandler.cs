using System.Threading.Tasks;
using Site.Application.Messaging;

namespace Site.Infrastructure.MessageHandlers
{
    public abstract class BaseMessageHandler<T> :  IMessageHandler<T> where T : IMessage
    {
        public abstract Task ProcessAsync(T message);

    }
}