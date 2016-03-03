using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectR.Models
{
    public class ConferenceModel
    {
        public int ConferenceId { get; set; }
        [Required]
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime Date { get; set; }
        [Required]
        public string Location { get; set; }
    }
}