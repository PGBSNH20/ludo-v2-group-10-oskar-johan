using Ludo_API.GameEngine.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Ludo_API.Models
{
    public class Player
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public Int32 ColorArgb
        {
            get
            {
                return Color.ToArgb();
            }
            set
            {
                Color = Color.FromArgb(value);
            }
        }

        #region Non-Mapped Properties
        [Required]
        [NotMapped]
        public Color Color { get; set; }

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

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;

            SetTrack();
        }
        #endregion

        public bool SetTrack()
        {
            if (Color.ToArgb() == Color.Gold.ToArgb())
            {
                Track = Gameboard.YellowTrack;
            }
            else if (Color.ToArgb() == Color.Red.ToArgb())
            {
                Track = Gameboard.RedTrack;
            }
            else if (Color.ToArgb() == Color.Blue.ToArgb())
            {
                Track = Gameboard.BlueTrack;
            }
            else if (Color.ToArgb() == Color.Green.ToArgb())
            {
                Track = Gameboard.GreenTrack;
            }
            else
            {
                throw new Exception("The player's color does not match any of allowed colours.");
                // todo: error handling?
            }

            StartPosition = Track[0];
            GoalIndex = Track[^1];
            return true;
        }
    }
}
