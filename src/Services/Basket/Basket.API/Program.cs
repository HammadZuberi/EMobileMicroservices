using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);
//Add Services to the  container


builder.Services.AddCarter();
var asemply = typeof(Program).Assembly;
builder.Services.AddMediatR( conf =>
{
    conf.RegisterServicesFromAssembly(asemply );
    conf.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    conf.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

var app = builder.Build();

// Configure the HTTP pipeline
app.MapCarter();

app.Run();