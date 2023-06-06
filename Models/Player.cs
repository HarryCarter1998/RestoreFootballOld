using RestoreFootball.Data.Services;

namespace RestoreFootball.Models
{
    public class Player
    {
        //private readonly IPlayerService _playerService;
        //public Player(IPlayerService playerService)
        //{
        //    _playerService = playerService;
        //    var dbPlayer = _playerService.Details(Id).Result;
        //    if(dbPlayer is not null)
        //    {
        //        Rating = dbPlayer.Rating;
        //        SignedUp = dbPlayer.SignedUp;
        //    }
        //    else
        //    {
        //        Rating = 5;
        //        SignedUp = false;
        //    }

            
        //}

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Rating { get; set; }
        public bool SignedUp { get; set; }
    }
}
