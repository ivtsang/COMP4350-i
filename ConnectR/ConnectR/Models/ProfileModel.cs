using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConnectR.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        {
            this.Conferences = new HashSet<Conference>();
            this.Messages = new HashSet<Message>();
            this.Participants = new HashSet<Participant>();
            this.Files = new HashSet<File>();
        }

        public int ProfileId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public Nullable<int> Age { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string School { get; set; }
        [Required]
        public string Degree { get; set; }
        public Nullable<int>  UserImage { get; set; }
        [MaxLength]
        public string About { get; set; }
        public bool Followed { get; set; }
        public int NumFollowers { get; set; }
        public int NumFollowing { get; set; }

        public virtual ICollection<Conference> Conferences { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}