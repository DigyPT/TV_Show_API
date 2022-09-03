using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TvShowAPI.Models
{
    public class User
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
       // public string Token { get; set; }



        public List<SerieUser> SerieUsers { get; set; }
    }
}
