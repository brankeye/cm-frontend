using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Objects;
using Newtonsoft.Json;

namespace cm.frontend.core.Domain.Services.Rest
{
    public class Schools : Base.Core<Domain.Models.School>
    {
        public Schools() : base("Schools")
        {
            
        }

        public virtual async Task<Domain.Models.School> GetAsync(string schoolName, string token = null)
        {
            var restUrl = BaseUri + "{0}";
            var target = TargetApi + "/" + schoolName;
            var uri = new Uri(string.Format(restUrl, target));

            Domain.Models.School model;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (token != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var result = await httpClient.GetStringAsync(uri);
                    var response = JsonConvert.DeserializeObject<Response>(result);
                    var item = response.Item.ToString();
                    model = JsonConvert.DeserializeObject<Domain.Models.School>(item);
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    model = null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    model = null;
                }
            }
            return model;
        }
    }
}
