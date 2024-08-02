
using CatalogAPI.Products.GetProduct;
using MediatR;

namespace CatalogAPI.Products.GetProducts
{


    //public record GetProductsRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{Category}", async (string Category,ISender sender) =>
            {
                var results = await sender.Send(new GetProductByCategoryQuery(Category));

                var response = results.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);


            }).WithName("GetProductByCategory")
            .WithSummary("Get Products by Category")
            .WithDescription("Get products by Category")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces<GetProductsResponse>(StatusCodes.Status200OK);
        }
    }
}
