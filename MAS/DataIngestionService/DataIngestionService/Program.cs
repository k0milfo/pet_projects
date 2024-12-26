using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProducer<string, string>>(provider =>
{
	var config = new ProducerConfig
	{
		BootstrapServers = builder.Configuration["Kafka:BootstrapServers"]
	};
	return new ProducerBuilder<string, string>(config).Build();
});

builder.Services.AddControllers();
//builder.Services.AddSwaggerGen();
var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
