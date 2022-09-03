using Microsoft.EntityFrameworkCore;

namespace TvShowAPI.Models
{
    [Keyless]
    public class ActorSerie
    {
        public int ActorsId { get; set; }
        public int SeriesId { get; set; }

        public Actor Actor { get; set; }
        public Serie Serie { get; set; }

    }
}
