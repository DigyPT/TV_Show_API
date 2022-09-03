using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TvShowAPI.Models
{
    public class Actor
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Date_Birthday { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public List<ActorSerie> ActorSeries { get; set; }
        //public List<ActorEpisode> ActorEpisodes { get; set; }
    }
}
