using Newtonsoft.Json;

namespace PublicHoliday.Model.Response
{
    public class SupportedCountryResponse
    {
        public SupportedCountryResponse()
        {
        }

        public SupportedCountryResponse(SupportedCountry supportedCountry)
        {
            CountryCode = supportedCountry.CountryCode;
            FullName = supportedCountry.FullName;
            HolidayTypes = supportedCountry.HolidayTypes.Select(p => p.Type).ToList();
            FromDate = new()
            {
                Day = supportedCountry.FromDay,
                Month = supportedCountry.FromMonth,
                Year = supportedCountry.FromYear
            };
            ToDate = new()
            {
                Day = supportedCountry.ToDay,
                Month = supportedCountry.ToMonth,
                Year = supportedCountry.ToYear
            };
            Regions = supportedCountry.Regions.Select(p => p.Name).ToList();
        }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("regions")]
        public List<string> Regions { get; set; }

        [JsonProperty("holidayTypes")]
        public List<string> HolidayTypes { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("fromDate")]
        public SupportedCountryResponseDate FromDate { get; set; }

        [JsonProperty("toDate")]
        public SupportedCountryResponseDate ToDate { get; set; }
    }
}