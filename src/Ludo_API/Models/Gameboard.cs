using System;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        //[Key]
        // https://stackoverflow.com/a/40917033
        //public int GameId { get; set; }

        //[Required]
        // https://stackoverflow.com/a/40917033
        public string GameId { get; set; }

        //[ForeignKey("LastPlayerId")]
        public Player LastPlayer { get; set; }
        //[ForeignKey("CurrentPlayerId")]
        public Player CurrentPlayer { get; set; }
        //[ForeignKey("GameCreatorId")]
        public Player GameCreator { get; set; }

        [Required]
        public List<Square> Squares { get; set; }

        [Required]
        public ICollection<Player> Players { get; set; }

        public DateTime? GameDate { get; set; } // todo: rename to something like "lastturndate"
        public DateTime? GameStartDate { get; set; }

        #region NotMappedVariables
        [NotMapped]
        public List<Player> OrderPlayers { get; set; }

        [NotMapped]
        public static List<int> YellowTrack { get; set; } = new();

        [NotMapped]
        public static List<int> RedTrack { get; set; } = new();

        [NotMapped]
        public static List<int> BlueTrack { get; set; } = new();

        [NotMapped]
        public static List<int> GreenTrack { get; set; } = new();

        [NotMapped]
        public List<string> ColorOrder = Player.GetValidColors();
        #endregion

        #region Constructors
        public Gameboard()
        {

        }

        public Gameboard(List<Player> players)
        {
            GameId = Guid.NewGuid().ToString();
            Players = players;
            Squares = new List<Square>();

            for (int i = 0; i < 60; i++)
            {
                Squares.Add(new Square
                {
                    ID = i,
                    //PieceCount = 0,
                    Tenant = new SquareTenant(i, null, 0),
                });
            }

            CreateOrderPlayers();
        }
        #endregion

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

        /// <summary>
        /// Get a Square with its index.
        /// </summary>
        /// <param name="index">The index of the Square to retrieve.</param>
        /// <returns></returns>
        public Square GetSquare(int index)
        {
            return Squares.ElementAtOrDefault(index);
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
