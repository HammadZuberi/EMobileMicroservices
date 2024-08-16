
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
    public class StoreBasketHandler(IBasketRepository _repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>

    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            //TODO: store baskte in Db (use martin upsert -if exist update if not insert
            //update cache
            await _repository.StoreBasket(cart, cancellationToken);
            var flag = string.IsNullOrEmpty(cart.UserName) ? false : true;

            return new StoreBasketResult(cart.UserName, flag);

        }
    }
}
