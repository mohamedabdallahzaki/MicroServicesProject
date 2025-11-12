using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var authSchema = "EShoppingGatewayAuthSchema";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(authSchema, options =>
    {
        options.Authority = "https://host.docker.internal:9009";
        options.Audience = "EShoppingGateway";

    });
      
builder.Configuration
       .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);


builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("CorsPolicy");
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello ocelot"); });
});

await app.UseOcelot();

await app.RunAsync();
