using System.ComponentModel.DataAnnotations;

namespace Ludo_WebApp.Models.DTO
{
    public class PlayerDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public PlayerDTO()
        {
        }
    }
}
