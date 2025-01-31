using Confluent.Kafka;
using DataIngestionService.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
