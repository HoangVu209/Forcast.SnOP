using Forcast.Models.Requests;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Forcast.Data.Infrastructure
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "https://localhost:7045";

        public BaseApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        private HttpClient CreateHttpClient()
        {
            return _httpClientFactory.CreateClient();
        }

        public async Task<RequestResponse> GetByIdAsync<T>(int id, string urlPath) where T : class
        {
            using var httpClient = CreateHttpClient();
            var response = await httpClient.GetAsync($"{_baseUrl}/{urlPath}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return new RequestResponse
                {
                    StatusCode = Code.OK,
                    Content = content
                };
            }
            else
            {
                return new RequestResponse
                {
                    StatusCode = Code.BadRequest,
                    Content = null,
                    Message = "Request failed with status: " + response.StatusCode
                };
            }
        }


        public async Task<T[]?> GetAllAsync<T>(string urlPath) where T : class
        {
            using var httpClient = CreateHttpClient();
            return await httpClient.GetFromJsonAsync<T[]>($"{_baseUrl}/{urlPath}");
        }

        public async Task<bool> AddAsync<T>(T entity, string urlPath) where T : class
        {
            using var httpClient = CreateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{_baseUrl}/{urlPath}", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync<T>(int id, T entity, string urlPath) where T : class
        {
            using var httpClient = CreateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{_baseUrl}/{urlPath}", entity);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> RemoveAsync(int id, string urlPath)
        {
            using var httpClient = CreateHttpClient();
            var response = await httpClient.DeleteAsync($"{_baseUrl}/{urlPath}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<RequestResponse> GetPagedAsync(string urlPath, int pageNumber, int pageSize)
        {
            using var httpClient = CreateHttpClient();
            var response = await httpClient.GetAsync($"{_baseUrl}/{urlPath}?pageNumber={pageNumber}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return new RequestResponse
                {
                    StatusCode = Code.OK,
                    Content = content
                };
            }
            else
            {
                return new RequestResponse
                {
                    StatusCode = Code.BadRequest,
                    Content = null,
                    Message = "Request failed with status: " + response.StatusCode
                };
            }
        }



    }
}
