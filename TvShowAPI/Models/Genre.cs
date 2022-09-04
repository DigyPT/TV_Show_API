using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
