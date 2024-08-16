
using Marten.Linq.SoftDeletes;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancelationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancelationToken);

            //trnary for basket exception
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancelationToken = default)
        {

            //upsert function for simplify we can update whole json as it stored in a document db
            session.Store(basket);
            await session.SaveChangesAsync(cancelationToken);
            return basket;

        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancelationToken = default)
        {

            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancelationToken);
            var result = session.IsDeleted(); //check if deleted
            return true;
        }

    }
}
