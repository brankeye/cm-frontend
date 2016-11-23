using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Rest.Security
{
    public class Account : Base.Core<Objects.Credentials>
    {
        public Account() : base("Account")
        {
            
        }

        public async Task<bool> PostRegisterAsync(Objects.Credentials credentials)
        {
            var response = await PostAsync(credentials, targetSection: "Register");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PostLogoutAsync(string token)
        {
            var response = await PostAsync(null, token, targetSection: "Logout");
            return response.IsSuccessStatusCode;
        }
    }
}
