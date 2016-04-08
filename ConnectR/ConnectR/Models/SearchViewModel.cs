using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConnectR.Models
{
    public class SearchViewModel
    {
        [Key]
        [Required]
        public string keyword { get; set; }
        public IEnumerable<Profile> profiles { set; get; }
        public IEnumerable<Conference> conferences { set; get; }
    }
}