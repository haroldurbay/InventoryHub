using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// GitHub Copilot assisted with configuring the API client and request timeout.
builder.Services.AddScoped(_ => new HttpClient
{
	BaseAddress = new Uri("http://localhost:5265/api/"),
	Timeout = TimeSpan.FromSeconds(10)
});
builder.Services.AddScoped<ProductCache>();

await builder.Build().RunAsync();
