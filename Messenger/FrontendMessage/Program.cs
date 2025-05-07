using Blazored.LocalStorage;
using FrontendMessage;
using FrontendMessage.Service.Implementations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddBlazoredLocalStorage();
var identityUrl = builder.Configuration["ApiSettings:IdentityUrl"];
var chatUrl = builder.Configuration["ApiSettings:ChatUrl"];
if (string.IsNullOrEmpty(identityUrl) || string.IsNullOrEmpty(chatUrl))
{
    throw new InvalidOperationException("IdentityUrl is not configured in appsettings.json.");
}

builder.Services.AddTransient<AuthenticationHandler>();
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .Or<TimeoutException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt));

builder.Services
    .AddHttpClient<ChatApiClient>(c => c.BaseAddress = new Uri(chatUrl))
    .AddHttpMessageHandler<AuthenticationHandler>()
    .AddPolicyHandler(retryPolicy);

builder.Services
    .AddHttpClient<IdentityApiClient>(client =>
    {
        client.BaseAddress = new Uri(identityUrl);
    })
    .AddPolicyHandler(retryPolicy);

builder.Services.AddMudServices();

await builder.Build().RunAsync();
