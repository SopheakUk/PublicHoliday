using PublicHoliday.Model.Response;

namespace PublicHoliday.Service.Interface
{
    public interface IPublicHolidayService
    {
        Task<List<SupportedCountryResponse>> GetCountryList();
        Task<List<MonthlyHolidayResponse>> GetMonthlyHoliday(int year, string country);
        Task<string> GetSpecificDayStatus(DateTime date, string country);
    }
}
