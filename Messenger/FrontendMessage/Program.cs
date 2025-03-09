using FrontendMessage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builderWebAssembly = WebAssemblyHostBuilder.CreateDefault(args);
builderWebAssembly.RootComponents.Add<App>("#app");
builderWebAssembly.RootComponents.Add<HeadOutlet>("head::after");
builderWebAssembly.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var baseUrl = builderWebAssembly.Configuration["ApiSettings:BaseUrl"];
if (string.IsNullOrEmpty(baseUrl))
{
    throw new InvalidOperationException("BaseUrl is not configured in appsettings.json.");
}

builderWebAssembly.Services.AddScoped(_ =>
{
    if (baseUrl != null)
    {
        return new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    throw new InvalidOperationException();
});
builderWebAssembly.Services.AddMudServices();

await builderWebAssembly.Build().RunAsync().ConfigureAwait(false);
