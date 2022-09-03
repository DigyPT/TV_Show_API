using System.Collections;
using System.Collections.Generic;

namespace TvShowAPI.Models
{
    public class Genre
    {
        public int ID { get; set; }
        [Required]
        public string GenreName { get; set; }

        
        public List<GenreSerie> GenreSeries { get; set; }

    }
}
