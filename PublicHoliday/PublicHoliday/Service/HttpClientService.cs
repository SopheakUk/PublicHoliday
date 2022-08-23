using Newtonsoft.Json;
using PublicHoliday.Model.Response;
using PublicHoliday.Service.Interface;

namespace PublicHoliday.Service
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> Get<TResponse>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            var content = await response.Content.ReadAsStringAsync();
            CheckError(content);

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        private static void CheckError(string content)
        {
            if (TryToDeserializeObject(content, out var errorResponse))
            {
                if (!string.IsNullOrWhiteSpace(errorResponse.Error))
                {
                    throw new Exception(errorResponse.Error);
                }
            }
        }

        private static bool TryToDeserializeObject(string content, out ErrorResponse errorResponse)
        {
            try
            {
                errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(content);
                return true;
            }
            catch { }
            errorResponse = null;
            return false;
        }
    }
}