using ForumBlog.Web.ApiServices.Interfaces;
using ForumBlog.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForumBlog.Web.ApiServices.Concrete
{
    public class AuthApiManager : IAuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;

        public AuthApiManager(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:54683/api/auth/");
            _accessor = accessor;
        }

        public async Task<bool> SignIn(AppUserLoginModel model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PostAsync("SignIn", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                AccessToken token = JsonConvert.DeserializeObject<AccessToken>(await responseMessage.Content.ReadAsStringAsync());

                _accessor.HttpContext.Session.SetString("token", token.Token);

                return true;
            }

            return false;

        }

    }
}
