
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName, bool IsSuccess);

    public class StoreBasketCmdValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCmdValidator()
        {

            RuleFor(x => x.Cart).NotNull().WithMessage(" Cart is required can not be empty");
            RuleFor(x => x.Cart.UserName).NotNull().WithMessage(" UserName is required");
        }
    }
    public class StoreBasketHandler(IBasketRepository _repository,
        DiscountProtoService.DiscountProtoServiceClient discountproto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>

    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {

          
            
            ShoppingCart cart = command.Cart;


            await DeductDiscount(cart, cancellationToken);
            // store basket in Db (use martin upsert -if exist update if not insert

            await _repository.StoreBasket(cart, cancellationToken);
            var flag = string.IsNullOrEmpty(cart.UserName) ? false : true;

            return new StoreBasketResult(cart.UserName, flag);

        }


        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {  //cummunicate with grpc and calculate latest prize 
            // Communicate with Discount.Grpc and calculate lastest prices of products into sc
            foreach (var item in cart.Items)
            {
                var coupon = await discountproto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
