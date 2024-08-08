
using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPut("products/",async (UpdateProductCommand request, ISender sender) =>
            {

                //var command = request.Adapt<UpdateProductCommand>(); 
                //same used one record only
                var result =await sender.Send(request);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            }).WithName("UpdateProduct")
            .WithSummary("UpdateProduct")
            .WithDescription("UpdateProduct products")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .Produces<GetProductsResponse>(StatusCodes.Status200OK);
        }
    }
}
