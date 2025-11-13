using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Repositories;
using Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.Authority = "https://id-local.eshopping.com:44344";
         options.RequireHttpsMetadata = true;

         options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidateAudience =  true,
             ValidAudience = "Catalog",
             ValidateIssuer = true,
             ValidIssuer = "https://id-local.eshopping.com:44344",
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ClockSkew = TimeSpan.Zero

         };
       

        // to connect with docker 
         options.BackchannelHttpHandler = new HttpClientHandler
         {
             ServerCertificateCustomValidationCallback = (messge, cert, chain, errors) => true
         };

         options.Events = new JwtBearerEvents
         {

             OnAuthenticationFailed = context =>
             {
                 Console.WriteLine($" Authentication Error");
                 Console.WriteLine($"{context.Exception.Message}");
                 Console.WriteLine($"{options.Authority}");

                 return Task.CompletedTask;
             }

         };

     });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("canRead", policy => policy.RequireClaim("scope", "catalogapi.read"));
});


builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);

var userPolicy = new AuthorizationPolicyBuilder()
     .RequireAuthenticatedUser().Build();

builder.Services.AddControllers(conf =>
{
    conf.Filters.Add(new AuthorizeFilter(userPolicy));
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    Assembly.GetAssembly(typeof(GetProductByIdQuery))));


builder.Services.AddScoped<ICatalogContext,CatalogContext>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "This is API for Catalog microservice in ecommerce application",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Mohamed Abdallah",
            Email = "Mohamedabdallah1542001@gmail.com",
            Url = new Uri("https://localhost:8000")
        }
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    // BEFORE UseSwagger / routing
    app.Use((ctx, next) =>
    {
        if (ctx.Request.Headers.TryGetValue("X-Forwarded-Prefix", out var p) && !string.IsNullOrEmpty(p))
            ctx.Request.PathBase = p.ToString();   // e.g., "/catalog"
        return next();
    });

    app.UseSwagger(c =>
    {
        // Make the OpenAPI "servers" base path match the prefix so Try it out uses /catalog/...
        c.PreSerializeFilters.Add((doc, req) =>
        {
            var prefix = req.Headers["X-Forwarded-Prefix"].FirstOrDefault();
            if (!string.IsNullOrEmpty(prefix))
                doc.Servers = new List<Microsoft.OpenApi.Models.OpenApiServer>
            { new() { Url = prefix } };
        });
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Catalog.API v1"); // relative path (no leading '/')
        c.RoutePrefix = "swagger";
    });

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
