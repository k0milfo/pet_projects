using System.IdentityModel.Tokens.Jwt;
using Pingo.Identity.Service.Entity.Options;
using Pingo.Identity.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton(typeof(JwtSecurityTokenHandler));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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
