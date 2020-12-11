using ForumBlog.Web.ApiServices.Interfaces;
using ForumBlog.Web.Extensions;
using ForumBlog.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ForumBlog.Web.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:54683/api/blogs/");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogListModel>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task AddAsync(BlogAddModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);

                ByteArrayContent byteContent = new ByteArrayContent(bytes);

                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(BlogAddModel.Image), model.Image.FileName);
            }

            var activeUser = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");

            model.AppUserId = activeUser.Id;

            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));

            formData.Add(new StringContent(model.ShortDescription.ToString()), nameof(BlogAddModel.ShortDescription));

            formData.Add(new StringContent(model.Description.ToString()), nameof(BlogAddModel.Description));

            formData.Add(new StringContent(model.Title.ToString()), nameof(BlogAddModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", formData);
        }


        public async Task UpdateAsync(BlogUpdateModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            if (model.Image != null)
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);

                ByteArrayContent byteContent = new ByteArrayContent(bytes);

                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(BlogUpdateModel.Image), model.Image.FileName);
            }

            var activeUser = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");

            model.AppUserId = activeUser.Id;

            formData.Add(new StringContent(model.Id.ToString()), nameof(BlogUpdateModel.Id));

            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogUpdateModel.AppUserId));

            formData.Add(new StringContent(model.ShortDescription.ToString()), nameof(BlogUpdateModel.ShortDescription));

            formData.Add(new StringContent(model.Description.ToString()), nameof(BlogUpdateModel.Description));

            formData.Add(new StringContent(model.Title.ToString()), nameof(BlogUpdateModel.Title));

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PutAsync($"{model.Id}", formData);
        }


    }
}
