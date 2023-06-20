namespace RestoreFootball.Models
{
    public class SameTeamRule
    {
        public int Id { get; set; }
        public ICollection<GameweekPlayer> GameweekPlayers { get; set; } = new List<GameweekPlayer>();
    }
}
