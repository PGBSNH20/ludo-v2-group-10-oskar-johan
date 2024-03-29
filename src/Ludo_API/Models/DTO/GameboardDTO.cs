﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Models.DTO
{
    public class GameboardDTO
    {
        public int ID { get; set; }
        public Player CurrentPlayer { get; set; }
        public ICollection<Square> Squares { get; set; }
        public ICollection<PlayerDTO> Players { get; set; }
        public DateTime? GameDate { get; set; } // todo: rename to something like "LastTurnDate"?
        public DateTime? GameStartDate { get; set; }
        public PlayerDTO GameCreator { get; set; }

        public GameboardDTO(Gameboard gameboard)
        {
            ID = gameboard.ID;
            CurrentPlayer = gameboard.CurrentPlayer;
            Squares = gameboard.Squares;
            Players = gameboard.Players?.Select(p => new PlayerDTO(p)).ToList();
            GameDate = gameboard.GameDate;
            GameStartDate = gameboard.GameStartDate;
            GameCreator = gameboard.GameCreator != null?new PlayerDTO(gameboard.GameCreator):null;
        }
    }
}

