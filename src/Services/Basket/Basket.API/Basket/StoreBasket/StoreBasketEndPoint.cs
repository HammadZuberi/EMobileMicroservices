
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName, bool IsSuccess);
    public class StoreBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {

                var comd = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(comd);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket {response.UserName} ", response);




            }).WithName("StoreBasket")
            .WithSummary(" StoreBasket")
            .WithDescription(" StoreBasket by username")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created);
        }
    }
}
