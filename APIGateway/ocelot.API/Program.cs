using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Configuration.AddJsonFile($"ocelot{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    app.MapGet("/", async context =>
    {
      context.Response.WriteAsync("Hello Ocelot");
    });
});

await app.UseOcelot();

await app.RunAsync();
