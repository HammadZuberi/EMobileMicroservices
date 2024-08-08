using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>
        (ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[START] handle request ={typeof(TRequest).Name} - Response={typeof(TResponse).Name} - RequestData={request}");

            //log request respond 
            //and time the incomming request


            var timer = new Stopwatch();
            timer.Start();

            //next pipepline
            var response = await next();

            timer.Stop();
            var timetaken = timer.Elapsed;//for performance concerns if request took more time than 3seconds
            if (timetaken.Seconds > 3)
            {
                logger.LogWarning($"[PERFORMANCE] the request ={typeof(TRequest).Name} - took ={timetaken.Seconds}");
            }


            logger.LogInformation($"[END] handled request ={typeof(TRequest).Name} - with Response={typeof(TResponse).Name} ");
            return response;

        }
    }
}
