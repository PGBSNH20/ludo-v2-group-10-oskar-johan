using Ludo_API.GameEngine.Game;
using Ludo_API.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;

namespace Ludo_API.Models
{
    public class Player
    {
        public const string ValidColorsPattern = "Yellow|Red|Blue|Green";

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        //[RegularExpression("^(Yellow | Red | Green | Blue)")]
        [RegularExpression("^(" + ValidColorsPattern + ")$")]
        public string Color { get; set; }

        #region static methods
        public static List<string> GetValidColors()
        {
            return ValidColorsPattern.Split("|").ToList();
        }
        #endregion static methods

        #region Non-Mapped Properties
        //[Required]
        //[NotMapped]
        //public Color Color { get; set; }

        [Required]
        [NotMapped]
        //public TrackData TrackData { get; set; }
        public ITrackData TrackData { get; set; }

        [NotMapped]
        public int StartPosition { get; set; }

        [NotMapped]
        public int GoalIndex { get; set; }

        [NotMapped]
        public List<int> Track { get; set; }

        #endregion

        #region Constructors
        public Player()
        {
            // todo: test if this is called before or after EF Core initializes the object
            //SetTrack();
        }

        //public Player(string name, Color color)
        public Player(string name, string color)
        {
            Name = name;
            Color = color;

            SetTrack();
        }

        //public Player(string name, Color color)
        public Player(NewPlayerDTO newPlayerDTO)
        {
            Name = newPlayerDTO.PlayerName;
            Color = newPlayerDTO.PlayerColor;

            SetTrack();
        }
        #endregion

        public bool SetTrack()
        {
            if (Color == "Yellow")
            {
                Track = Gameboard.YellowTrack;
            }
            else if (Color == "Red")
            {
                Track = Gameboard.RedTrack;
            }
            else if (Color == "Blue")
            {
                Track = Gameboard.BlueTrack;
            }
            else if (Color == "Green")
            {
                Track = Gameboard.GreenTrack;
            }
            else
            {
                throw new Exception("The player's color does not match any of allowed colours.");
            }

            StartPosition = Track[0];
            GoalIndex = Track[^1];
            return true;
        }
    }
}
