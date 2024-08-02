
using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.UpdateProduct
{
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPut("products/{Id}", async (Guid Id, ISender sender) =>
            {

                //var command = request.Adapt<UpdateProductCommand>(); 
                //same used one record only
                var result = sender.Send(new DeleteProductCommand(Id));

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            }).WithName("DeleteProduct")
            .WithSummary("DeleteProduct")
            .WithDescription("DeleteProduct products")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK);
        }
    }
}
