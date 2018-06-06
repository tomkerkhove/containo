using System.Threading.Tasks;
using Containo.Services.Orders.Storage.Caching.Interfaces;
using Containo.Services.Orders.Storage.Contracts.v1;
using Containo.Services.Orders.Storage.Repositories.Interfaces;

namespace Containo.Services.Orders.Storage.Repositories
{
    public class CachedOrdersRepository : ICachedReadOrdersRepository
    {
        private readonly ICache cache;
        private readonly IReadOrdersRepository ordersRepository;

        public CachedOrdersRepository(IReadOrdersRepository ordersRepository, ICache cache)
        {
            this.ordersRepository = ordersRepository;
            this.cache = cache;
        }

        public async Task<OrderRecord> GetAsync(string customerName, string confirmationId)
        {
            var cacheKey = $"{customerName}-{confirmationId}".ToUpper();
            var cachedOrder = await cache.GetAsync<OrderRecord>(cacheKey);
            if (cachedOrder != null)
            {
                return cachedOrder;
            }

            var order = await ordersRepository.GetAsync(customerName, confirmationId);
            await cache.SetAsync(cacheKey, order);

            return order;
        }
    }
}