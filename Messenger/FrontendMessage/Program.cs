using FrontendMessage;
using FrontendMessage.Service.Implementations;
using FrontendMessage.Service.Interface;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Polly;
using Polly.Extensions.Http;

var builderWebAssembly = WebAssemblyHostBuilder.CreateDefault(args);
builderWebAssembly.RootComponents.Add<App>("#app");
builderWebAssembly.RootComponents.Add<HeadOutlet>("head::after");
builderWebAssembly.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var baseUrl = builderWebAssembly.Configuration["ApiSettings:BaseUrl"];
var chatUrl = builderWebAssembly.Configuration["ApiSettings:ChatUrl"];
if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(chatUrl))
{
    throw new InvalidOperationException("BaseUrl is not configured in appsettings.json.");
}

builderWebAssembly.Services.AddScoped<ITokenStorage, BrowserTokenStorage>();
builderWebAssembly.Services.AddTransient<AuthenticationHandler>();
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .Or<TimeoutException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt));

builderWebAssembly.Services
    .AddHttpClient<ChatApiClient>(c => c.BaseAddress = new Uri(chatUrl))
    .AddHttpMessageHandler<AuthenticationHandler>()
    .AddPolicyHandler(retryPolicy);

builderWebAssembly.Services
    .AddHttpClient<MainApiClient>(client =>
    {
        client.BaseAddress = new Uri(baseUrl);
    })
    .AddHttpMessageHandler<AuthenticationHandler>()
    .AddPolicyHandler(retryPolicy);

builderWebAssembly.Services.AddMudServices();

await builderWebAssembly.Build().RunAsync();
