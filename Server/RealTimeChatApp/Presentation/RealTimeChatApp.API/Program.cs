using RealTimeChatApp.Application.Abstractions.Services.RabbitMq;
using RealTimeChatApp.Infrastructure.Services.Concrete.RabbitMq;
using RealTimeChatApp.Redis;
using RealTimeChatApp.SignalR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalRServices();
builder.Services.AddRedisServices();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(x => true)));

var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .Filter.ByExcluding(e => e.MessageTemplate.Text.Contains("Request starting") || e.MessageTemplate.Text.Contains("Request finished"))
    .CreateLogger();
builder.Host.UseSerilog(log);
builder.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();
builder.Services.AddHostedService<RabbitMQConsumerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();

app.MapControllers();
app.MapHub<ChatHub>("/chat-hub");
app.Run();