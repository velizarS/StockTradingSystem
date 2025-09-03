using System.Threading.Tasks;

namespace PortfolioService.Messaging
{
    public interface IEventConsumer
    {
        Task StartConsumingAsync();
    }
}
