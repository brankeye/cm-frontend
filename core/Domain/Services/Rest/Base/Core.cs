using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Objects;
using Newtonsoft.Json;

namespace cm.frontend.core.Domain.Services.Rest.Base
{
    public class Core<TModel>
        where TModel : class
    {
        private string BaseUri => "http://ec2-52-39-4-189.us-west-2.compute.amazonaws.com/api/";
        private readonly string _targetApi;

        public Core(string targetApi)
        {
            _targetApi = targetApi;
        }

        public virtual async Task<List<TModel>> GetAsync(string token = null, string basicCredentials = null)
        {
            var restUrl = BaseUri + "{0}";
            var uri = new Uri(string.Format(restUrl, _targetApi));

            List<TModel> jsonValues;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (token != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (basicCredentials != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicCredentials);

                try
                {
                    var result = await httpClient.GetStringAsync(uri);
                    var response = JsonConvert.DeserializeObject<Response>(result);
                    var item = response.Item.ToString();
                    jsonValues = JsonConvert.DeserializeObject<List<TModel>>(item);
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    jsonValues = new List<TModel>();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    jsonValues = new List<TModel>();
                }
            }
            return jsonValues;
        }

        public virtual async Task<HttpResponseMessage> PostAsync(TModel model, string token = null, string targetSection = "")
        {
            var restUrl = BaseUri + "{0}";
            var target = _targetApi;
            if (string.IsNullOrEmpty(targetSection) == false)
            {
                target += "/" + targetSection;
            }
            var uri = new Uri(string.Format(restUrl, target));

            var json = "";
            if (model != null)
            {
                json = JsonConvert.SerializeObject(model);
            }
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (token != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                response = await httpClient.PostAsync(uri, content);
            }
            return response;
        }

        public virtual async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var restUrl = BaseUri + "{0}/{1}";
            var uri = new Uri(string.Format(restUrl, _targetApi));

            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                response = await httpClient.DeleteAsync(uri);
            }
            return response;
        }
    }
}
