using System.Threading.Tasks;
using Containo.Services.Orders.Contracts.Messaging.v1;
using Containo.Services.Orders.Storage;
using Containo.Services.Orders.Storage.Repositories;

namespace Containo.Services.Orders.QueueProcessor
{
    public class Program : MessagePump<OrderMessage>
    {
        private readonly OrdersRepository ordersRepository = new OrdersRepository();
        protected override async Task ProcessMessageAsync(string correlationId, string cycleId, OrderMessage message)
        {
            await ordersRepository.StoreAsync(message.CustomerName, message.ConfirmationId, message.ProductId, message.Amount);
        }

        private static async Task Main(string[] args)
        {
            var messagePump = new Program();
            await messagePump.ReceiveMessagesAsync();
        }
    }
}