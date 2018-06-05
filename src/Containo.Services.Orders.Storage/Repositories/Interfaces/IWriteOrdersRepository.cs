using System.Threading.Tasks;

namespace Containo.Services.Orders.Storage
{
    public interface IWriteOrdersRepository
    {
        Task StoreAsync(string customerName, string confirmationId, int productId, int amount);
    }
}