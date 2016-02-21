using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectR.Models
{
    public class ProfileModel
    {
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
    }
}