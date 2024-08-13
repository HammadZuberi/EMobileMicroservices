
namespace Basket.API.Basket.GetBasket
{

    //public record GetBasketQuery(string UserName);

    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {//delegate handlewr
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);

            }).WithName("GetBasket")
            .WithSummary(" Get Basket")
            .WithDescription(" Get Basket by username")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .Produces<GetBasketResponse>(StatusCodes.Status200OK);
        }
    }
}
