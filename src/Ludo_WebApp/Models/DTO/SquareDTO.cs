namespace Ludo_WebApp.Models.DTO
{
    public class SquareDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public SquareTenantDTO Tenant { get; set; }

        public SquareDTO()
        {

        }
    }
}
