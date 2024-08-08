using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handler;
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
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
//configure http request pipeline
app.MapCarter();

app.UseExceptionHandler( options => { });

app.Run();
