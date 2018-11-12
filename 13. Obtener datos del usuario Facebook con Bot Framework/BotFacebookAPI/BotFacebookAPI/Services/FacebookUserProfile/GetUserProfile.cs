using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace BotFacebookAPI.Services.FacebookUserProfile
{
    public class GetUserProfile
    {
        public async Task<UserProfileModel> Execute(string idUser)
        {
            Uri baseUri = new Uri("https://graph.facebook.com");
            string fields = "?fields=id,name,first_name,last_name,profile_pic,locale,timezone,gender";
            string accessToken = "";
            Uri uri = new Uri(baseUri, (idUser + fields + "&access_token=" + accessToken));            

            UserProfileModel model = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<UserProfileModel>(result);
                }
                return model;
            }
        }
    }
}