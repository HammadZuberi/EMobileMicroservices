using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handler;
using CatalogAPI.Data;
using HealthChecks.UI.Client;
var builder = WebApplication.CreateBuilder(args);

//ADd services


var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));    //generic <,>
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));    //generic logging for  <,>
});
var Dbconfig = builder.Configuration.GetConnectionString("Database");
builder.Services.AddMarten(opt =>
{
    opt.Connection(Dbconfig!);
}).UseLightweightSessions();


if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogIntialData>();
}

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks().AddNpgSql(Dbconfig!);

var app = builder.Build();
//configure http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
