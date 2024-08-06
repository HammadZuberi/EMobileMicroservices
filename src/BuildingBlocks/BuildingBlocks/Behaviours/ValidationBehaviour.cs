using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildingBlocks.Behaviours
{
    //handle all the request in one handle prim CTOR
    //checks for all validation in the incomming request and throw if any errors
    //ensuring only valid request 
    public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
                    IPipelineBehavior<TRequest, TResponse>
              where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // triggers all validation for a request

            var context = new ValidationContext<TRequest>(request);

            var validationRequest = await Task.WhenAll(
                            validators.Select(v =>
                            v.ValidateAsync(context, cancellationToken)));


            var failure = validationRequest.
                            Where(r => r.Errors.Any()).
                            SelectMany(r => r.Errors).ToList();

            if (failure.Any())
            {
                throw new ValidationException(failure);
            }

            //return next pipeline
            return await next();

        }
    }
}
