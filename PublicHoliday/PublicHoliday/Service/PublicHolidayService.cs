using Microsoft.EntityFrameworkCore;
using PublicHoliday.Model;
using PublicHoliday.Model.Response;
using PublicHoliday.Repository.Interface;
using PublicHoliday.Service.Interface;

namespace PublicHoliday.Service
{
    public class PublicHolidayService : IPublicHolidayService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IPublicHolidayRepository _publicHolidayRepository;

        public PublicHolidayService(IHttpClientService httpClientService,
                                    IPublicHolidayRepository publicHolidayRepository)
        {
            _httpClientService = httpClientService;
            _publicHolidayRepository = publicHolidayRepository;
        }

        public async Task<string> GetSpecificDayStatus(DateTime date, string country)
        {
            if (await IsPublicHolidy(date, country)) return "holiday";
            return await IsWorking(date, country) ? "workday" : "free day";
        }

        public async Task<List<GroupMonthlyHolidayResponse>> GetGroupMonthlyHoliday(int year, string country)
        {
            return ConvertToGroupMonthlyHolidayResponses(await GetMonthlyHoliday(year, country));
        }

        public async Task<int> GetMaximumNumberOfFree(int year, string country)
        {
            return (await GetMonthlyHoliday(year, country)).Count;
        }

        public async Task<List<SupportedCountryResponse>> GetCountryList()
        {
            if (_publicHolidayRepository.SupportedCountry.Any())
            {
                return ConvertToSupportedCountriesResponse(_publicHolidayRepository.SupportedCountry);
            }

            var url = "https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries";
            var response = await _httpClientService.Get<List<SupportedCountryResponse>>(url);

            var supportedCountries = ConvertToSupportedCountries(response);
            _publicHolidayRepository.InsertSupportedCountries(supportedCountries);

            return response;
        }

        private async Task<bool> IsPublicHolidy(DateTime date, string country)
        {
            var url = $"https://kayaposoft.com/enrico/json/v2.0/?action=isPublicHoliday&date={date:dd-MM-yyyy}&country={country}";
            var response = await _httpClientService.Get<PublicHolidayResponse>(url);
            return response.IsPublicHoliday;
        }

        private async Task<bool> IsWorking(DateTime date, string country)
        {
            var url = $"https://kayaposoft.com/enrico/json/v2.0/?action=isWorkDay&date={date:dd-MM-yyyy}&country={country}";
            var response = await _httpClientService.Get<WorkingDayResponse>(url);
            return response.IsWorkDay;
        }

        private async Task<List<MonthlyHoliday>> GetMonthlyHoliday(int year, string country)
        {
            var monthlyHolidaysFromDb = _publicHolidayRepository.MonthlyHoliday.Where(p => p.Year == year && p.Country == country);

            if (monthlyHolidaysFromDb.Any())
            {
                return monthlyHolidaysFromDb.ToList();
            }

            var url = $"https://kayaposoft.com/enrico/json/v2.0/?action=getHolidaysForYear&year={year}&country={country}";
            var response = await _httpClientService.Get<List<MonthlyHolidayResponse>>(url);

            var monthlyHolidays = ConvertToMonthlyHolidays(response, country);
            _publicHolidayRepository.InsertMonthlyHolidays(monthlyHolidays);

            return monthlyHolidays;
        }

        private static List<MonthlyHoliday> ConvertToMonthlyHolidays(List<MonthlyHolidayResponse> response, string country)
        {
            return response.Select(p => new MonthlyHoliday(p, country)).ToList();
        }

        private static List<GroupMonthlyHolidayResponse> ConvertToGroupMonthlyHolidayResponses(
            IEnumerable<MonthlyHoliday> monthlyHolidays)
        {
            return monthlyHolidays.GroupBy(p => p.Month).Select(p => new GroupMonthlyHolidayResponse()
            {
                Month = p.Key,
                Days = p.OrderBy(x => x.Day).Select(q => q.Day).ToList()
            }).OrderBy(p => p.Month).ToList();
        }

        private static List<SupportedCountry> ConvertToSupportedCountries(List<SupportedCountryResponse> response)
        {
            return response.Select(p => new SupportedCountry(p)).ToList();
        }

        private static List<SupportedCountryResponse> ConvertToSupportedCountriesResponse(
            DbSet<SupportedCountry> supportedCountries)
        {
            return supportedCountries.Select(p => new SupportedCountryResponse(p)).ToList();
        }
    }
}