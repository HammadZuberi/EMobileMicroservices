using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using BuildingBlocks.Behaviours;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

//ADd services


var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));    //generic <,>
});
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

var app = builder.Build();
//configure http request pipeline

app.MapCarter();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (error == null) return;
        //parse the ewxc with Json 
        var problemdetails = new ProblemDetails
        {
            Title = error.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = error.StackTrace,

        };

        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(error, error.Message);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        // using static System.Net.Mime.MediaTypeNames;
        context.Response.ContentType = "application/problem+json";
        

        await context.Response.WriteAsJsonAsync(problemdetails);

    });
});

app.Run();
