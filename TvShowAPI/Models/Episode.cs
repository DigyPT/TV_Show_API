using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TvShowAPI.Models
{
    public class Episode
    {
        
        public int ID { get; set; }

        [Required]
        public int Episode_Number { get; set; }
        [Required]
        public int Season { get; set; }
        [Required]
        public string Episode_Name { get; set; }
        public string Air_Date { get; set; }

        [Required]
        public int SerieId { get; set; }

        public ICollection<Actor> Actors { get; set; }
        //public List<ActorEpisode> ActorEpisodes { get; set; }


    }
}
