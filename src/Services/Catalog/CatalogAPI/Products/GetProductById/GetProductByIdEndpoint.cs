
using CatalogAPI.Products.GetProduct;
using MediatR;

namespace CatalogAPI.Products.GetProducts
{


    //public record GetProductsRequest();
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{Id}", async (Guid Id,ISender sender) =>
            {
                var results = await sender.Send(new GetProductByIdQuery(Id));

                var response = results.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);


            }).WithName("GetProductById")
            .WithSummary("Get Products by id")
            .WithDescription("Get products by id")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<GetProductsResponse>(StatusCodes.Status200OK);
        }
    }
}
