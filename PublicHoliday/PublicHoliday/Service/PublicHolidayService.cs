using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public async Task<List<MonthlyHolidayResponse>> GetMonthlyHoliday(int year, string country)
        {
            var monthlyHolidaysFromDb = _publicHolidayRepository.MonthlyHoliday.Where(p => p.Year == year && p.Country == country);

            if (monthlyHolidaysFromDb.Any())
            {
                return ConvertToMonthlyHolidayResponses(monthlyHolidaysFromDb);
            }

            var url = $"https://kayaposoft.com/enrico/json/v2.0/?action=getHolidaysForYear&year={year}&country={country}";
            var response = await _httpClientService.Get(url);
            var monthlyHolidayResponses = await ConvertToMonthlyHolidayResponses(response);

            var monthlyHolidays = ConvertToMonthlyHolidays(monthlyHolidayResponses, country);
            _publicHolidayRepository.InsertMonthlyHolidays(monthlyHolidays);

            return monthlyHolidayResponses;
        }

        private static async Task<List<MonthlyHolidayResponse>> ConvertToMonthlyHolidayResponses(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(content);
            if (!string.IsNullOrWhiteSpace(errorResponse.Error))
            {
                throw new Exception(errorResponse.Error);
            }

            var monthlyHolidayResponses = JsonConvert.DeserializeObject<List<MonthlyHolidayResponse>>(content);
            return monthlyHolidayResponses;
        }

        private static List<MonthlyHoliday> ConvertToMonthlyHolidays(List<MonthlyHolidayResponse> response, string country)
        {
            return response.Select(p => new MonthlyHoliday(p, country)).ToList();
        }

        private static List<MonthlyHolidayResponse> ConvertToMonthlyHolidayResponses(IQueryable<MonthlyHoliday> monthlyHolidays)
        {
            return monthlyHolidays.Select(p => new MonthlyHolidayResponse(p)).ToList();
        }

        public async Task<List<SupportedCountryResponse>> GetCountryList()
        {
            if (_publicHolidayRepository.SupportedCountry.Any())
            {
                return ConvertToSupportedCountriesResponse(_publicHolidayRepository.SupportedCountry);
            }

            var url = "https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries";
            var response = await _httpClientService.Get(url);

            var supportedCountryResponses = await ConvertToSupportedCountryResponses(response);

            var supportedCountries = ConvertToSupportedCountries(supportedCountryResponses);
            _publicHolidayRepository.InsertSupportedCountries(supportedCountries);

            return supportedCountryResponses;
        }

        private async Task<List<SupportedCountryResponse>> ConvertToSupportedCountryResponses(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SupportedCountryResponse>>(content);
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