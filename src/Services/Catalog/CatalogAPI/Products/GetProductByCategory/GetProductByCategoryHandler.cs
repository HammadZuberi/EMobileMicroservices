namespace CatalogAPI.Products.GetProduct
{

    public record GetProductByCategoryQuery(string Category)
        : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    //using primary ctor and directly as marten is already absttract 

    //internal class CreateProductCmdHandler
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            //buissness logig to create a product
            logger.LogInformation($"GetProduct categoryQueryHandler called with query {query}");

            var product = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken);

            //if (product is null)
            //{
            //    throw new ProductNotFoundException();
            //}
            return new GetProductByCategoryResult(product);
        }
    }
}
