using Discount.Api.services;
using Discount.Application.Command;
using Discount.Application.Mapper;
using Discount.Core.Repositoires;
using Discount.Infrastructure.Extension;
using Discount.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(DiscountProfile).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(CreateDiscountCommond))));

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

await  app.MigrateDatabase();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountServices>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("commonction wirh grpc endpoints must be made through a grpc client");
    });
});

app.Run();
