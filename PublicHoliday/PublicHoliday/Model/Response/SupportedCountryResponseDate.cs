using Newtonsoft.Json;

namespace PublicHoliday.Model.Response
{
    public class SupportedCountryResponseDate
    {
        public SupportedCountryResponseDate()
        {
        }

        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}