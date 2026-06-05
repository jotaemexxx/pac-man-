using Microsoft.AspNetCore.SignalR;
using PacMan.Hubs;

namespace PacMan.Services
{
    public class GameLoopService : BackgroundService
    {
        private readonly GameService _gameService;
        private readonly IHubContext<GameHub> _hubContext;

        public GameLoopService(GameService gameService, IHubContext<GameHub> hubContext)
        {
            _gameService = gameService;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var start = DateTime.UtcNow;

                if (!_gameService.EndGame)
                {
                    _gameService.MoveGhosts();

                    if (_gameService.CheckCollision())
                    {
                        _gameService.EndGame = true;
                        await _hubContext.Clients.All.SendAsync("gameOver", cancellationToken: stoppingToken);
                    }
                    else
                    {
                        await _hubContext.Clients.All.SendAsync("updateState", new
                        {
                            pacman = new
                            {
                                nodeId = _gameService.Pacman.CurrentNode.Id,
                                row = _gameService.Pacman.CurrentNode.Row,
                                col = _gameService.Pacman.CurrentNode.Col,
                                score = _gameService.Pacman.Score
                            },
                            ghosts = _gameService.Ghosts.Select(g => new {
                                nodeId = g.CurrentNode.Id,
                                row = g.CurrentNode.Row,
                                col = g.CurrentNode.Col,
                                color = g.Color
                            }),
                            dots = _gameService.Dots
                        }, cancellationToken: stoppingToken);
                    }
                }

                var elapsed = (DateTime.UtcNow - start).TotalMilliseconds;
                var remaining = 400 - (int)elapsed;
                if (remaining > 0)
                    await Task.Delay(remaining, stoppingToken);
            }
        }
    }
}