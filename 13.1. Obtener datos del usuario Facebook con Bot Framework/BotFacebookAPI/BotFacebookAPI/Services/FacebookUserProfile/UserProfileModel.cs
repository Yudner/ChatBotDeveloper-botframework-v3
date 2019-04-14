using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotFacebookAPI.Services.FacebookUserProfile
{
    public class UserProfileModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Profile_pic { get; set; }
        public string Locale { get; set; }
        public string Timezone { get; set; }
        public string Gender { get; set; }
    }
}
