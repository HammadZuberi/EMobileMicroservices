


namespace Basket.API.Basket.GetBasket
{

    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            // TODO get Db form dat
            //.. var basket = await _repository.GEtBasket(

            return new GetBasketResult(new ShoppingCart("SWN"));
        }
    }
}
