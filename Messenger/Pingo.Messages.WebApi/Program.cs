using Pingo.Messages.Extensions;
using Pingo.Messages.WebApi.Entity;
using Pingo.Messages.WebApi.Service.Implementations;
using Pingo.Messages.WebApi.Service.Interface;
using Index = FrontendMessage.Pages.Index;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IMessagesService<Index.MessageFrontend>, MessagesService>();
builder.Services.AddMessages(builder.Configuration);
builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin() // Разрешить запросы с любого источника
    .AllowAnyMethod() // Разрешить любые методы (GET, POST и т.д.)
    .AllowAnyHeader()));
builder.Services.AddAuthorization();
builder.Services.AddControllers(); // для swagger
var app = builder.Build();
app.UseCors("AllowAll");
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

await app.RunAsync().ConfigureAwait(false);

namespace Pingo.Messages.WebApi
{
    public sealed partial class Program
    {
        public static Program CreateInstance()
        {
            return new Program();
        }
    }
}
