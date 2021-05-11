using Ludo_API.Database;
using Ludo_API.Models;
using Ludo_API.Models.DTO;
using Ludo_API.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.GameEngine.Game
{
    public class PlayerManager
    {
        internal List<Models.Player> CreatePlayers(LudoContext context, IPlayerRepository playerRepository, GameOptions gameOptions)
        {
            List<Models.Player> players = new();

            foreach (var playerDTO in gameOptions.PlayersDTO)
            {
                // Validate Input (or earlier)?
                // Convert Colors?
                players.Add(new Models.Player
                {
                    Name = playerDTO.Name,
                    Color = Color.FromArgb(playerDTO.Color),
                });
            }

            playerRepository.AddPlayers(context, players);
            // ??? var players = context.Players.Where(p => p.gameId == gameId);
            return players;
        }
    }
}
