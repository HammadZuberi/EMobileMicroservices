namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        //contract
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancelationToken = default);
        Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancelationToken = default);
        Task<bool> DeleteBasket(string userName, CancellationToken cancelationToken = default);
    }
}
