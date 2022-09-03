using Microsoft.EntityFrameworkCore;

namespace TvShowAPI.Models
{
    [Keyless]
    public class ActorEpisode
    {
        public int ActorID { get; set; }
        public int EpisodeId {get; set; }

        //public Actor Actor { get; set; }
        //public Episode Episode { get; set; }
    }
}
