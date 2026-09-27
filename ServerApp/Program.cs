var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("ProductList", policy =>
        policy.Expire(TimeSpan.FromSeconds(60)));
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5268", "https://localhost:7059")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseOutputCache();

var products = new[]
{
    new Product(1, "Laptop", 1200.50m, 25, new Category(1, "Computers")),
    new Product(2, "Headphones", 50.00m, 100, new Category(2, "Audio"))
};

// GitHub Copilot assisted with modeling the nested category returned for each product.
app.MapGet("/api/productlist", () => products)
    .CacheOutput("ProductList");

app.Run();

record Product(int Id, string Name, decimal Price, int Stock, Category Category);

record Category(int Id, string Name);
