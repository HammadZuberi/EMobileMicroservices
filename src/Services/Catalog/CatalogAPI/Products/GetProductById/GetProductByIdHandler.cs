

namespace CatalogAPI.Products.GetProduct
{

    public record GetProductByIdQuery(Guid Id)
        : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    //using primary ctor and directly as marten is already absttract 

    //internal class CreateProductCmdHandler
    internal class GetProductByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
