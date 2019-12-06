using System.Threading.Tasks;

namespace Site.Application.Interfaces.Messaging
{
    public interface IMessageHandler<IMessage>
    {
        Task ProcessAsync(IMessage message);
    }
}