namespace RestoreFootball.Models
{
    public class GameweekPlayer
    {
        public int Id { get; set; }
        public Team Team { get; set; }

        public virtual required Player Player { get; set; }
        public virtual required Gameweek Gameweek { get; set; }
    }
}
