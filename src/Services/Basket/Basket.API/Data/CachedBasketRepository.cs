
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    // to implement the core logic in from IBasketRepo inject in the class and inherit to use the samne
    // CachedBasketRepository act as a procxy and forward the call to basket repo
    //decorator patern to exnd for cache 
    public class CachedBasketRepository(IBasketRepository _repository, IDistributedCache cache) : IBasketRepository
    {

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancelationToken = default)
        {
            //for cache key
            var cachedBasket = await cache.GetStringAsync(userName, cancelationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;


            //perform database call
            var basket = await _repository.GetBasket(userName, cancelationToken);
            //set cache 
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancelationToken);
            return basket;

        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancelationToken = default)
        {
            var basketstore = await _repository.StoreBasket(basket, cancelationToken);

            //set cache 
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancelationToken);

            return basketstore;

        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancelationToken = default)
        {

            var resp = await _repository.DeleteBasket(userName, cancelationToken);
            //remove from cache
            await cache.RemoveAsync(userName, cancelationToken);
            return resp;
        }

    }
}
