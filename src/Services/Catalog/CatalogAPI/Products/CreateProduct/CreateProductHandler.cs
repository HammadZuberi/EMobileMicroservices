
namespace CatalogAPI.Products.CreateProduct
{

    public record CreateProductCommand(string Name,
        List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);


    //using primary ctor and directly as marten is already absttract 

    //internal class CreateProductCmdHandler
    internal class CreateProductCmdHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>  
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //buissness logig to create a product
            //TODO
            //save to Db
            //return result


            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);
          await  session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.ID);
        }
    }
}
