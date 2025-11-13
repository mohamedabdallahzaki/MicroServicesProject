using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Mappers;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Common.Logging;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.Authority = "https://id-local.eshopping.com:44344";
         options.RequireHttpsMetadata = true;

         options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidateAudience = true,
             ValidAudience = "Basket",
             ValidateIssuer = true,
             ValidIssuer = "https://id-local.eshopping.com:44344",
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ClockSkew = TimeSpan.Zero

         };
         // for connect with doxker 

         options.BackchannelHttpHandler = new HttpClientHandler
         {
             ServerCertificateCustomValidationCallback = (messge, cert, chain, errors) => true
         };

         options.Events = new JwtBearerEvents
         {

             OnAuthenticationFailed = context =>
             {
                 Console.WriteLine($"======= Authentication Error");
                 Console.WriteLine($"{context.Exception.Message}");
                 Console.WriteLine($"{options.Authority}");

                 return Task.CompletedTask;
             }

         };

     });


builder.Services.AddAutoMapper(typeof(BasketMappingProfile).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    Assembly.GetAssembly(typeof(CreateShoppingCartCommand))));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    cfg => cfg.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ct, cfg) =>
    {

        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

builder.Services.AddMassTransitHostedService();

var userPolicy = new AuthorizationPolicyBuilder()
     .RequireAuthenticatedUser().Build();

builder.Services.AddControllers(conf =>
{
    conf.Filters.Add(new AuthorizeFilter(userPolicy));
});


builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
}).AddApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl= true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Basket API",
        Version = "v1",
        Description = "This is API for basket microservice in ecommerce application",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Mohamed Abdallah",
            Email = "Mohamedabdallah1542001@gmail.com",
            Url = new Uri("https://localhost:8000")
        }
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Basket API",
        Version = "v2",
        Description = "This is API for basket microservice v2 in ecommerce application",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Mohamed Abdallah",
            Email = "Mohamedabdallah1542001@gmail.com",
            Url = new Uri("https://localhost:8000")
        }
    });

    options.DocInclusionPredicate((version, apiDescription) =>
    {
        if (!apiDescription.TryGetMethodInfo(out var methodInfo))
        {
            return false;
        }
        var versions = methodInfo.DeclaringType?
                       .GetCustomAttributes(true)
                       .OfType<ApiVersionAttribute>()
                       .SelectMany(a => a.Versions);
           

        return versions?.Any(v => $"v{v.ToString()}" == version) ?? false;

    
    });
});
//redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});



var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

var nginxPath = "/basket";
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.Use((ctx, next) =>
    {
        if(ctx.Request.Headers.TryGetValue("X-ForwardedFor-Prefix", out var prefix) && !string.IsNullOrEmpty(prefix))
        {
            ctx.Request.PathBase = prefix.ToString();
        }

        return next();
    });
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"{nginxPath}/swagger/v1/swagger.json", "Basket.APi v1");
        c.SwaggerEndpoint($"{nginxPath}/swagger/v2/swagger.json", "Basket.APi v2");
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
