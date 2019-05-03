using System.Threading.Tasks;

namespace Site.Application.Messaging
{
    public interface IMessageHandler<IMessage>
    {
        Task ProcessAsync(IMessage message);
    }
}