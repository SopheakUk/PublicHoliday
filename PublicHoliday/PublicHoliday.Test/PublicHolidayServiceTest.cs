using Microsoft.EntityFrameworkCore;
using NSubstitute;
using PublicHoliday.Model;
using PublicHoliday.Model.Response;
using PublicHoliday.Repository.Interface;
using PublicHoliday.Service;
using PublicHoliday.Service.Interface;

namespace PublicHoliday.Test
{
    public class PublicHolidayServiceTest
    {
        private IHttpClientService _httpClientService;
        private IPublicHolidayRepository _publicHolidayRepository;
        private IPublicHolidayService _publicHolidayService;

        [SetUp]
        public void Setup()
        {
            _httpClientService = Substitute.For<IHttpClientService>();
            _publicHolidayRepository = Substitute.For<IPublicHolidayRepository>();
            _publicHolidayService = new PublicHolidayService(_httpClientService, _publicHolidayRepository);

            MockSupportedCountry();
            MockMonthlyHoliday();
        }

        private void MockSupportedCountry()
        {
            var data = new List<SupportedCountry>().AsQueryable();
            var mockSet = Substitute.For<DbSet<SupportedCountry>, IQueryable<SupportedCountry>>();
            ((IQueryable<SupportedCountry>)mockSet).Provider.Returns(data.Provider);
            ((IQueryable<SupportedCountry>)mockSet).Expression.Returns(data.Expression);
            ((IQueryable<SupportedCountry>)mockSet).ElementType.Returns(data.ElementType);
            ((IQueryable<SupportedCountry>)mockSet).GetEnumerator().Returns(data.GetEnumerator());

            _publicHolidayRepository.SupportedCountry.Returns(mockSet);
        }

        private void MockMonthlyHoliday()
        {
            var data = new List<MonthlyHoliday>().AsQueryable();
            var mockSet = Substitute.For<DbSet<MonthlyHoliday>, IQueryable<MonthlyHoliday>>();
            ((IQueryable<MonthlyHoliday>)mockSet).Provider.Returns(data.Provider);
            ((IQueryable<MonthlyHoliday>)mockSet).Expression.Returns(data.Expression);
            ((IQueryable<MonthlyHoliday>)mockSet).ElementType.Returns(data.ElementType);
            ((IQueryable<MonthlyHoliday>)mockSet).GetEnumerator().Returns(data.GetEnumerator());

            _publicHolidayRepository.MonthlyHoliday.Returns(mockSet);
        }

        [Test]
        public async Task GetCountryList_should_request_specific_url()
        {
            var expectedUrl = "https://kayaposoft.com/enrico/json/v2.0/?action=getSupportedCountries";

            _httpClientService.Get<List<SupportedCountryResponse>>(Arg.Any<string>())
             .Returns(new List<SupportedCountryResponse>());

            await _publicHolidayService.GetCountryList();

            await _httpClientService.Received().Get<List<SupportedCountryResponse>>(expectedUrl);
        }

        [Test]
        public async Task GetGroupMonthlyHoliday_should_request_specific_url()
        {
            var expectedUrl = "https://kayaposoft.com/enrico/json/v2.0/?action=getHolidaysForYear&year=2022&country=ago";

            _httpClientService.Get<List<MonthlyHolidayResponse>>(Arg.Any<string>())
             .Returns(new List<MonthlyHolidayResponse>());

            await _publicHolidayService.GetGroupMonthlyHoliday(2022, "ago");

            await _httpClientService.Received().Get<List<MonthlyHolidayResponse>>(expectedUrl);
        }
    }
}