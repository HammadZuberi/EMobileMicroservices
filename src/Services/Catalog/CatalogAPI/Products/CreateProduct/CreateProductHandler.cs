
namespace CatalogAPI.Products.CreateProduct
{

    public record CreateProductCommand(string Name,
        List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

        }
    }

    //using primary ctor and directly as marten is already absttract 
    //internal class CreateProductCmdHandler
    internal class CreateProductCmdHandler(IDocumentSession session, ILogger<CreateProductCmdHandler> logger, IValidator<CreateProductCommand> validator)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //buissness logig to create a product
            //save to Db    
            //return result

            //buissness logig to create a product
            logger.LogInformation($"CreateProductCmdHandler called with get cmd {command}");


            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.ID);
        }
    }
}
