

namespace CatalogAPI.Products.GetProduct
{

    public record GetProductByIdQuery(Guid Id)
        : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
   
    //using primary ctor and directly as marten is already absttract 

    //internal class CreateProductCmdHandler
    internal class GetProductByIdQueryHandler(IDocumentSession session,ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>  
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            //buissness logig to create a product
            logger.LogInformation("GetProductQueryHandler called with get cmd");
           
            var product= await session.LoadAsync<Product>(query.Id,cancellationToken);

            if (product is null) {

                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(product);
        }
    }
}
