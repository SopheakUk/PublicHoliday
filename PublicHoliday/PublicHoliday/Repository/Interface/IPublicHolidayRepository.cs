using Microsoft.EntityFrameworkCore;
using PublicHoliday.Model;

namespace PublicHoliday.Repository.Interface
{
    public interface IPublicHolidayRepository
    {
        DbSet<SupportedCountry> SupportedCountry { get; set; }
        DbSet<MonthlyHoliday> MonthlyHoliday { get; set; }

        void InsertMonthlyHolidays(List<MonthlyHoliday> monthlyHolidays);

        void InsertSupportedCountries(List<SupportedCountry> supportedCountries);
    }
}