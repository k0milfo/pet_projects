using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Monitoring_Service.DAL;
using Monitoring_Service.DAL.Interfaces;
using Monitoring_Service.DAL.Repositories;
using Monitoring_Service.Domain.Entity;
using Monitoring_Service.Service.Interfaces;
using MonitoringServiceDatabase.Controller;
using MonitoringServiceProducer.Controller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDatabaseService<SensorData>, DatabaseService>();

builder.Services.AddSingleton<IConsumer<string, string>>(consumer =>
{
	var config = new ConsumerConfig
	{
		BootstrapServers = builder.Configuration["Kafka:BootstrapServers"],
		GroupId = "unified-consumer-group",
		AutoOffsetReset = AutoOffsetReset.Earliest
	};
	return new ConsumerBuilder<string, string>(config).Build();
});

builder.Services.AddSingleton<MonitoringController>();
builder.Services.AddSingleton<DataBaseController>();

builder.Services.AddControllers();

var app = builder.Build();

var router = app.Services.GetService<IRouterService>();
var cts = new CancellationTokenSource();
Task.Run(() => router.RouteMessage(cts.Token));


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
