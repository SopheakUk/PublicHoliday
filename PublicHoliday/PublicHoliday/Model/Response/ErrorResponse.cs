using Newtonsoft.Json;

namespace PublicHoliday.Model.Response
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}