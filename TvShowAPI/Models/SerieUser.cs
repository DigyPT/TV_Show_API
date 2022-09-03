using Microsoft.EntityFrameworkCore;

namespace TvShowAPI.Models
{
    [Keyless]
    public class SerieUser
    {
        public int SerieID { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }
        public Serie Serie { get; set; }
    }
}
