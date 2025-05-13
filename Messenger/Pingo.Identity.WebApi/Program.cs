using System.IdentityModel.Tokens.Jwt;
using Hangfire;
using Hangfire.Redis.StackExchange;
using MassTransit;
using MassTransit.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Pingo.Identity.Service.Entity.Options;
using Pingo.Identity.Service.Extensions;
using Pingo.Identity.WebApi.Entity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity();
builder.Services.AddHangfire(config =>
    config.UseRedisStorage(builder.Configuration.GetConnectionString("Redis")));
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<JwtSecurityTokenHandler>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin() // Разрешить запросы с любого источника
    .AllowAnyMethod() // Разрешить любые методы (GET, POST и т.д.)
    .AllowAnyHeader()));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
        cfg.Host(settings.Host, h =>
        {
            h.Username(settings.Login);
            h.Password(settings.Password);
        });
    });
});
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSource(DiagnosticHeaders.DefaultListenerName) // MassTransit(RabbitMQ)
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: "Pingo.Identity.Service"))
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(builder.Configuration["Jaeger:Host"]!);
            options.Protocol = OtlpExportProtocol.Grpc;
        }));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHangfireDashboard();  // Доступно по URL https://localhost:7294/hangfire
await app.RunAsync();

namespace Pingo.Identity.WebApi
{
    public sealed partial class Program
    {
        public static Program CreateInstance()
        {
            return new Program();
        }
    }
}
