using FrontendMessage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
if (string.IsNullOrEmpty(baseUrl))
{
    throw new InvalidOperationException("BaseUrl is not configured in appsettings.json.");
}

builder.Services.AddScoped(_ =>
{
    if (baseUrl != null)
    {
        return new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    throw new InvalidOperationException();
});
builder.Services.AddMudServices();

await builder.Build().RunAsync().ConfigureAwait(false);
