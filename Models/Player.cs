
namespace RestoreFootball.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Rating { get; set; } = 5;
        public bool SignedUp { get; set; } = false;
        public Team Team { get; set; } = (Team)Enum.GetValues(typeof(Team)).GetValue(new Random().Next(Enum.GetValues(typeof(Team)).Length));
    }

    public enum Team { Yellow, Green, Orange, NonBibs }
}
