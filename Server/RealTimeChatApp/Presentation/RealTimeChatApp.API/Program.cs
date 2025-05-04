using RealTimeChatApp.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalRServices();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(x => true)));
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
