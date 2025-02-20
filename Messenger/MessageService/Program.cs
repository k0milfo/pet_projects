using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Pingo.Messages.Entity;
using Pingo.Messages.Service;
using Pingo.Messages.Service.Interfaces;
using Pingo.Messages.Service.Repositories;
using Pingo.Messages.WebApi.Controllers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("MessagerDb"));
builder.Services.AddScoped<MessagesController>();
builder.Services.AddScoped<IMessageDataRepository<Message>, MessageDataRepository>();
builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin() // Разрешить запросы с любого источника
    .AllowAnyMethod() // Разрешить любые методы (GET, POST и т.д.)
    .AllowAnyHeader()));
builder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddSerilog(dispose: true));
var app = builder.Build();
app.UseCors("AllowAll");
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateLogger();

await app.RunAsync().ConfigureAwait(false);

public sealed partial class Program
{
    public static Program CreateInstance()
    {
        return new Program();
    }
}
