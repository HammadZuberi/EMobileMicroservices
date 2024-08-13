
using Basket.API.Basket.StoreBasket;
using FluentValidation.Results;

namespace Basket.API.Basket.DeleteBasket
{

    //public record DeleteBasketRequest(string UserName);

    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {


                var result = await sender.Send(new DeleteBasketCommand(userName));

                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok( response);


            }).WithName("DeleteBasket")
            .WithSummary("Delete Basket")
            .WithDescription(" Delete Basket by username")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .Produces<DeleteBasketEndPoint>(StatusCodes.Status200OK);
        }
    }
}
