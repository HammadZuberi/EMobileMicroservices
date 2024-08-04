var builder = WebApplication.CreateBuilder(args);

//ADd services

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
var app = builder.Build();
//configure http request pipeline

app.MapCarter();
app.Run();
