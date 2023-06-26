namespace RestoreFootball.Models
{
    public class Grouping
    {
        public int Id { get; set; }
        public ICollection<GameweekPlayer> GameweekPlayers { get; set; } = new List<GameweekPlayer>();
    }
}
