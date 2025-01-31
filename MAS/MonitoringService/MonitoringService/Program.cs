<<<<<<< HEAD
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Monitoring_Service;
using Monitoring_Service.DAL;
using Monitoring_Service.DAL.Interfaces;
using Monitoring_Service.DAL.Repositories;
using Monitoring_Service.Domain.Entity;
using Monitoring_Service.Service.Implementations;
using Monitoring_Service.Service.Interfaces;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISensorDataRepository<SensorData>, SensorDataRepository>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
//builder.Services.AddScoped<IConsumer<string, string>>(consumer =>
//{
//	var config = new ConsumerConfig
//	{
//		BootstrapServers = builder.Configuration["Kafka:BootstrapServers"],
//		GroupId = "unified-consumer-group",
//		AutoOffsetReset = AutoOffsetReset.Earliest
//	};
//	return new ConsumerBuilder<string, string>(config).Build();
//});

builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
{
	HostName = "rabbitmq",
	UserName = "guest",
	Password = "guest"
});

//builder.Services.AddScoped<IProducer<string, string>>(producer =>
//{
//	var config = new ProducerConfig
//	{
//		BootstrapServers = builder.Configuration["Kafka:BootstrapServers"]
//	};
//	return new ProducerBuilder<string, string>(config).Build();
//});
//builder.Services.AddScoped<RouterService>();
//builder.Services.AddSingleton<MonitoringController>(); builder.Services.AddHostedService<RouterService>();
//builder.Services.AddSingleton<DataBaseController>();
//builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();
builder.Services.AddHostedService<RouterService>();
builder.Services.AddScoped<ISensorDataChecker, SensorDataChecker>();
//builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI();
//var router = app.Services.GetService<IRouterService>();
//var cts = new CancellationTokenSource();
//Task.Run(() => router.RouteMessage(cts.Token));
//if (app.Environment.IsDevelopment())
//{
//	Console.WriteLine("Environment is Development. Applying migrations...");


app.ApplyMigrations();


//}
//else
//{
//	Console.WriteLine($"Environment is {app.Environment.EnvironmentName}. Skipping migrations.");
//}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
=======
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
>>>>>>> 7a9adf1f8c04b7d7e362b4edabbc562e1a0f96d9
