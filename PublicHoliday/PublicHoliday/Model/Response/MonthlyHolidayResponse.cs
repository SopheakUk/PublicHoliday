using Newtonsoft.Json;

namespace PublicHoliday.Model.Response
{
    public class MonthlyHolidayResponse
    {
        public MonthlyHolidayResponse(MonthlyHoliday monthlyHoliday)
        {
            Date = new(monthlyHoliday);
            Names = monthlyHoliday.Names.Select(p => new MonthlyHolidayResponseName(p)).ToList();
            HolidayType = monthlyHoliday.HolidayType;
        }

        public MonthlyHolidayResponse()
        {
        }

        [JsonProperty("date")]
        public MonthlyHolidayResponseDate Date { get; set; }

        [JsonProperty("name")]
        public List<MonthlyHolidayResponseName> Names { get; set; }

        [JsonProperty("holidayType")]
        public string HolidayType { get; set; }
    }

    public class MonthlyHolidayResponseName
    {
        public MonthlyHolidayResponseName()
        {
        }

        public MonthlyHolidayResponseName(MonthlyHolidayName monthlyHolidayName)
        {
            Lang = monthlyHolidayName.Lang;
            Text = monthlyHolidayName.Text;
        }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class MonthlyHolidayResponseDate
    {
        public MonthlyHolidayResponseDate()
        {
        }

        public MonthlyHolidayResponseDate(MonthlyHoliday monthlyHoliday)
        {
            Day = monthlyHoliday.Day;
            DayOfWeek = monthlyHoliday.DayOfWeek;
            Month = monthlyHoliday.Month;
            Year = monthlyHoliday.Year;
        }

        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("dayOfWeek")]
        public int DayOfWeek { get; set; }
    }
}