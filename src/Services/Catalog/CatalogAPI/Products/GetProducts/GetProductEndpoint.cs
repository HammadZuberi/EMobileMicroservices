
using CatalogAPI.Products.GetProduct;

namespace CatalogAPI.Products.GetProducts
{


    public record GetProductsRequest(int? PageNumber, int? PageSize = 10);
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request,ISender sender) =>
            {


               var query = request.Adapt<GetProductQuery>();
                var results = await sender.Send(query);

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
