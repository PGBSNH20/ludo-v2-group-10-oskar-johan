﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;

namespace Ludo_API.Models
{
    public class Gameboard
    {
        [Key]
        public int ID { get; set; }

        public Player LastPlayer { get; set; }

        [Required]
        public List<Square> Squares { get; set; }

        [Required]
        public List<Player> Players { get; set; }

        public DateTime GameDate { get; set; }

        #region NotMappedVariables
        [NotMapped]
        public List<Player> OrderPlayers { get; set; }

        [NotMapped]
        public static List<int> YellowTrack = new();

        [NotMapped]
        public static List<int> RedTrack = new();

        [NotMapped]
        public static List<int> BlueTrack = new();

        [NotMapped]
        public static List<int> GreenTrack = new();

        [NotMapped]
        public List<Color> ColorOrder = new() { Color.Yellow, Color.Red, Color.Blue, Color.Green };
        #endregion

        public Gameboard()
        {

        }

        public static void CreateTracks()
        {
            YellowTrack.AddRange(Enumerable.Range(0, 45));

            RedTrack.AddRange(Enumerable.Range(10, 30));
            RedTrack.AddRange(Enumerable.Range(0, 10));
            RedTrack.AddRange(Enumerable.Range(45, 5));

            BlueTrack.AddRange(Enumerable.Range(20, 20));
            BlueTrack.AddRange(Enumerable.Range(0, 20));
            BlueTrack.AddRange(Enumerable.Range(50, 5));

            GreenTrack.AddRange(Enumerable.Range(30, 10));
            GreenTrack.AddRange(Enumerable.Range(0, 30));
            GreenTrack.AddRange(Enumerable.Range(55, 5));
        }

        public Gameboard(List<Player> players)
        {
            Players = players;
            Squares = new List<Square>();
            GameDate = DateTime.Now;

            for (int i = 0; i < 60; i++)
            {
                Squares.Add(new Square
                {
                    ID = i,
                    PieceCount = 0
                });
            }

            CreateOrderPlayers();
        }

        public void CreateOrderPlayers()
        {
            OrderPlayers = (from player in Players
                            join color in ColorOrder on player.Color equals color
                            orderby ColorOrder.FindIndex(x => x == color)
                            select player).ToList();
        }

        public Player NextPlayer(Player player)
        {
            int playerIndex = OrderPlayers.FindIndex(p => p == player);

            return playerIndex + 1 < OrderPlayers.Count ? OrderPlayers[playerIndex + 1] : OrderPlayers[0];
        }
    }
}