using Asp.Versioning;
using Basket.Applicaation.Commonds;
using Basket.Applicaation.GrpcServices;
using Basket.Applicaation.Mapping;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Proto3;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(typeof(ShoppingCartMappingprofile).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),Assembly.GetAssembly(typeof (CreateBasketCommond))
    ));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(cfg =>

  cfg.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"])

);
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});


builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {

        Title = "Basket Api",
        Version = "v1",
        Description = "This is api for basket in ecommerce app",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Mohamed Abdallah Zaki",
            Email = "mohamedabdallh1542001@.com",
            Url = new Uri("https://github.com/mohamedabdallahzaki")
        }
    });
});
// redis caching 

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
