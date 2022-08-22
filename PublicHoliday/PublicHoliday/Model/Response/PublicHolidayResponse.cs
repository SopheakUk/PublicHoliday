using Newtonsoft.Json;

namespace PublicHoliday.Model.Response
{
    public class PublicHolidayResponse
    {
        [JsonProperty("isPublicHoliday")]
        public bool IsPublicHoliday { get; set; }
    }
}
