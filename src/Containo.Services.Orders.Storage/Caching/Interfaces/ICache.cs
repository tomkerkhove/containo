using System.Threading.Tasks;

namespace Containo.Services.Orders.Storage.Caching.Interfaces
{
    public interface ICache
    {
        Task<TCacheItem> GetAsync<TCacheItem>(string cacheKey) where TCacheItem : class;
        Task SetAsync<TCacheItem>(string cacheKey, TCacheItem cacheItem) where TCacheItem : class;
    }
}