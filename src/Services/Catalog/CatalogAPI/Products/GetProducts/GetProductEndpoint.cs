
using CatalogAPI.Products.GetProduct;

namespace CatalogAPI.Products.GetProducts
{


    //public record GetProductsRequest();
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var results = await sender.Send(new GetProductQuery());

                var response = results.Adapt<GetProductsResponse>();

                return Results.Ok(response);


            }).WithName("GetProduct")
            .WithSummary("Get Products")
            .WithDescription("Get products")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<GetProductsResponse>(StatusCodes.Status201Created);
        }
    }
}
