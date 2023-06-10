using System.ComponentModel.DataAnnotations.Schema;

namespace RestoreFootball.Models
{
    public class Gameweek
    {
        public int Id { get; set; }
        public int GreenScore { get; set; }
        public int NonBibsScore { get; set; }
        public int YellowScore { get; set; }
        public int OrangeScore { get; set; }

        public virtual ICollection<GameweekPlayer>? GameweekPlayers { get; set; }
    }
}
