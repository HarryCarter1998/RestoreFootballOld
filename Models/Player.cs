
namespace RestoreFootball.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Rating { get; set; } = 5;
        public bool SignedUp { get; set; } = false;
        public Team Team { get; set; } = 0;
    }

    public enum Team { Yellow, Green, Orange, NonBibs }
}
