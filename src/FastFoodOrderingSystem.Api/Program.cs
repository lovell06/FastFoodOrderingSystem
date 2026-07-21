using FastFoodOrderingSystem.Api.Extensions;
using FastFoodOrderingSystem.Application;
using FastFoodOrderingSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddPresentation();

var app = builder.Build();

app.UsePresentation();

app.Run();