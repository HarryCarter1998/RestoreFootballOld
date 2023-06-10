namespace RestoreFootball.Models
{
    public class GameweekPlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Team Team { get; set; }

        public virtual Player? Player { get; set; }
        public virtual Gameweek? Gameweek { get; set; }
    }
}
