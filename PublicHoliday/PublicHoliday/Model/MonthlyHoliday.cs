using PublicHoliday.Model.Response;

namespace PublicHoliday.Model
{
    public class MonthlyHoliday : EntityBase
    {
        public MonthlyHoliday()
        {
        }

        public MonthlyHoliday(MonthlyHolidayResponse response, string country)
        {
            Day = response.Date.Day;
            Month = response.Date.Month;
            HolidayType = response.HolidayType;
            DayOfWeek = response.Date.DayOfWeek;
            Year = response.Date.Year;
            Country = country;
            Names = response.Names.Select(p => new MonthlyHolidayName(p)).ToList();
        }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int DayOfWeek { get; set; }

        public virtual List<MonthlyHolidayName> Names { get; set; }

        public string HolidayType { get; set; }
        public string Country { get; set; }
    }

    public class MonthlyHolidayName : EntityBase
    {
        public MonthlyHolidayName(MonthlyHolidayResponseName responseName)
        {
            Lang = responseName.Lang;
            Text = responseName.Text;
        }

        public MonthlyHolidayName()
        {
        }

        public string Lang { get; set; }

        public string Text { get; set; }
    }
}