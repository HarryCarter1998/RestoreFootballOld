namespace RestoreFootball.Models
{
    public class Gameweek
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today.AddDays(((int)DayOfWeek.Wednesday - (int)DateTime.Today.DayOfWeek + 7) % 7);
        public int? GreenScore { get; set; }
        public int? NonBibsScore { get; set; }
        public int? YellowScore { get; set; }
        public int? OrangeScore { get; set; }

        public virtual ICollection<GameweekPlayer>? GameweekPlayers { get; set; }
    }
}
