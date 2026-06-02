using PacMan.Services;
using PacMan.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 1. Registra o SignalR
builder.Services.AddSignalR();

// 2. Registra o GameService como Singleton
// Singleton = uma única instância para todos os jogadores
builder.Services.AddSingleton<GameService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Serve arquivos estáticos (nosso frontend)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

// 3. Mapeia o Hub no endpoint /gameHub
app.MapHub<GameHub>("/gameHub");

GraphTestService test = new GraphTestService();
test.Run();

app.Run();