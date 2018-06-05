using System.Threading.Tasks;
using Containo.Services.Orders.Storage.Contracts.v1;

namespace Containo.Services.Orders.Storage.Repositories.Interfaces
{
    public interface IReadOrdersRepository
    {
        Task<OrderRecord> GetAsync(string customerName, string confirmationId);
    }
}