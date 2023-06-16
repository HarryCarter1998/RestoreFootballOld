using System.Text.Json.Serialization;

namespace RestoreFootball.Models
{
    public class GameweekPlayer
    {
        public int Id { get; set; }
        public Team Team { get; set; }

        [JsonPropertyName("player")]
        public virtual required Player Player { get; set; }
        [JsonPropertyName("gameweek")]
        public virtual required Gameweek Gameweek { get; set; }
    }

    public enum Team { Green, NonBibs, Yellow, Orange }
}
