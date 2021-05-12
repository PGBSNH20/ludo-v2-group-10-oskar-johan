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

        [NotMapped]
        public Color Color { get; set; }

        #region Non-Mapped Properties
        [NotMapped]
        public int StartPosition { get; set; }

        [NotMapped]
        public int GoalIndex { get; set; }

        [NotMapped]
        public List<int> Track { get; set; }
        #endregion

        public Player()
        {

        }

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;

            SetTrack();
        }

        public void SetTrack()
        {
            if (Color.ToArgb() == Color.Yellow.ToArgb())
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
                // todo: error handling?
            }

            StartPosition = Track[0];
            GoalIndex = Track[^1];
        }
    }
}
