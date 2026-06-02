using Microsoft.AspNetCore.SignalR;
using PacMan.Services;

namespace PacMan.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }
        public async Task MovePlayer(int destination)
        {
            _gameService.MovePlayer(destination);
            _gameService.MoveGhosts();

            if (_gameService.CheckCollision())
            {
                await Clients.All.SendAsync("gameOver");
                return;
            }

            if (_gameService.CheckWin())
            {
                await Clients.All.SendAsync("gameWin");
                return;
            }

            await Clients.All.SendAsync("updateState", new
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
            });
        }

        public async Task GetState()
        {
            await Clients.Caller.SendAsync("updateState", new
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
            });
        }
    }
}