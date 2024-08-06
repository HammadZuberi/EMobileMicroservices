
namespace CatalogAPI.Products.UpdateProduct
{

    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductValidate : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidate()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");

        }
    }


    //inject Idocument seession for injecting in primary cotor
    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($" update command looger with command {command}");
            //var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            //if (product == null)
            //    throw new ProductNotFoundException();
            //session.Delete(product);

            session.Delete(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);

        }
    }
}
