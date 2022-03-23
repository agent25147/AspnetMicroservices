using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BaskedRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BaskedRepository(IDistributedCache cache)
        {
            this._redisCache = cache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basketJson = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basketJson))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basketJson);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

      
    }
}
