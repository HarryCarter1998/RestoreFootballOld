using System.Text.Json.Serialization;

namespace RestoreFootball.Models
{
    public class Gameweek
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today.AddDays(7);
        public int? GreenScore { get; set; }
        public int? NonBibsScore { get; set; }
        public int? YellowScore { get; set; }
        public int? OrangeScore { get; set; }

        [JsonIgnore]
        public virtual ICollection<GameweekPlayer>? GameweekPlayers { get; set; } = new List<GameweekPlayer>();
        [JsonIgnore]
        public virtual ICollection<SameTeamRule>? SameTeamRules { get; set; } = new List<SameTeamRule>();
    }
}
