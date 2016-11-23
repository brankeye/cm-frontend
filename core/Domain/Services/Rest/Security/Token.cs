using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace cm.frontend.core.Domain.Services.Rest.Security
{
    public class Token
    {
        private string BaseUri => "http://ec2-52-39-4-189.us-west-2.compute.amazonaws.com/";
        private string TargetApi = "Token";

        public virtual async Task<Objects.Token> PostAsync(string username, string password)
        {
            var restUrl = BaseUri + "{0}";
            var uri = new Uri(string.Format(restUrl, TargetApi));

            var credentials = "grant_type=password&username=" + username + "&password=" + password;
            var content = new StringContent(credentials, Encoding.UTF8, "application/json");

            Objects.Token token = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var item = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<Objects.Token>(item);
                }
            }
            return token;
        }
    }
}
