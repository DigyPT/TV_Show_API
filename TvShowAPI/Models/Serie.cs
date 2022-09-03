using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvShowAPI.Models
{
    public class Serie
    {
        
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        public string rating { get; set; }

       
        public ICollection<Episode>Episodes { get; set; }
      


        public List<ActorSerie> ActorSeries { get; set; }
        public List<GenreSerie> GenreSeries { get; set; }
        public List<SerieUser> SerieUsers { get; set; }
    }
}
