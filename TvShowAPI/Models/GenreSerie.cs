using Microsoft.EntityFrameworkCore;

namespace TvShowAPI.Models
{
    [Keyless]
    public class GenreSerie
    {
        public int GenreId { get; set; }
        public int SerieId { get; set; }

        public Serie Serie { get; set; }
        public Genre Genre { get; set; }
    }
}
