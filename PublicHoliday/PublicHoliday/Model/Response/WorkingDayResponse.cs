using Newtonsoft.Json;

namespace PublicHoliday.Model.Response
{
    public class WorkingDayResponse
    {
        [JsonProperty("isWorkDay")]
        public bool IsWorkDay { get; set; }
    }
}
