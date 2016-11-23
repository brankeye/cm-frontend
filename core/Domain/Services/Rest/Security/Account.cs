using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest.Security
{
    public class Account : Base.Core<Objects.Credentials>
    {
        public Account() : base("Account")
        {
            
        }

        public async Task<HttpResponseMessage> PostRegisterAsync(string username, string password)
        {
            var credentials = new Domain.Objects.Credentials
            {
                Email = username,
                Password = password,
                ConfirmPassword = password
            };

            var response = await PostAsync(credentials, targetSection: "Register");
            return response;
        }

        public async Task<HttpResponseMessage> PostLogoutAsync(string token)
        {
            var response = await PostAsync(null, token, targetSection: "Logout");
            return response;
        }
    }
}
