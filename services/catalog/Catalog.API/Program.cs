var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {

        Title = "Catalog Api",
        Version = "v1",
        Description = "This is api for catalog in ecommerce app",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Mohamed Abdallah Zaki",
            Email = "mohamedabdallh1542001@.com",
            Url = new Uri("https://github.com/mohamedabdallahzaki")
        }
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
