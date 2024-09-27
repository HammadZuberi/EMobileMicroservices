using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//add services to the container


///----------------
/////infrastructure -EFCore
///Application -Miadtr
///APi- carter healthchecks
///

builder.Services.AddAPIServices()
	.AddInfrastructureServices(builder.Configuration)
	.AddApplicationServices();


var app = builder.Build();
//conmfigure Http request pipeline

app.Run();
