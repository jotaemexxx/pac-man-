using PacMan.Services;
using PacMan.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddSignalR();


builder.Services.AddSingleton<GameService>();


builder.Services.AddHostedService<GameLoopService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();


app.MapHub<GameHub>("/gameHub");

GraphTestService test = new GraphTestService();
test.Run();

app.Run();