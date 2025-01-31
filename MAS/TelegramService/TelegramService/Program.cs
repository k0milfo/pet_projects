using RabbitMQ.Client;
using TelegramService.Controllers;
using TelegramService.Service.Implementations;
using TelegramService.Service.Interfaces;
using TelegramService.DAL;
using Microsoft.EntityFrameworkCore;
using TelegramService.DAL.Interfaces;
using TelegramService.DAL.Repositories;
using TelegramService.Domain.Entity;
using Monitoring_Service;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddHostedService<TelegramAlert>(); // äëÿ ïîëó÷åíèÿ ChatId
builder.Services.AddScoped<ITelegramAlert, TelegramAlert>(); // ÂÊËÞ×ÈÒÜ ÏÎÑËÅ ÏÎËÓ×ÅÍÈß chatId

//builder.Services.AddSingleton<ITelegramRegistration>(sp => new TelegramRegistration("7729053840:AAEy_AM_QnowZWiwCVJ-bXBgMJqmKxIeFVw"));
builder.Services.AddSingleton<ITelegramRegistration>(sp =>
{
	var token = "7729053840:AAEy_AM_QnowZWiwCVJ-bXBgMJqmKxIeFVw";
	var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
	return new TelegramRegistration(token, scopeFactory);
});
builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
{
	HostName = "rabbitmq",
	UserName = "guest",
	Password = "guest"
});
builder.Services.AddHostedService<NotificationController>();
var app = builder.Build();

app.ApplyMigrations();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
