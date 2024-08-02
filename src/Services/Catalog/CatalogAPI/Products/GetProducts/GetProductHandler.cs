
namespace CatalogAPI.Products.GetProduct
{

    public record GetProductQuery()
        : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
   
    //using primary ctor and directly as marten is already absttract 

    //internal class CreateProductCmdHandler
    internal class GetProductQueryHandler(IDocumentSession session,ILogger<GetProductQueryHandler> logger)
    : IQueryHandler<GetProductQuery, GetProductResult>  
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            //buissness logig to create a product
            logger.LogInformation("GetProductQueryHandler called with get cmd");
           
            var products= await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductResult(products);
        }
    }
}
