using Microsoft.EntityFrameworkCore;
using PublicHoliday.Model;
using PublicHoliday.Repository.Interface;

namespace PublicHoliday.Repository
{
    public class PublicHolidayRepository : DbContext, IPublicHolidayRepository
    {
        private readonly IConfiguration _configuration;

        public PublicHolidayRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(_configuration.GetConnectionString("Db"));
        }

        public DbSet<SupportedCountry> SupportedCountry { get; set; }
        public DbSet<MonthlyHoliday> MonthlyHoliday { get; set; }

        public void InsertSupportedCountries(List<SupportedCountry> supportedCountries)
        {
            SupportedCountry.AddRange(supportedCountries);
            SaveChanges();
        }

        public void InsertMonthlyHolidays(List<MonthlyHoliday> monthlyHolidays)
        {
            MonthlyHoliday.AddRange(monthlyHolidays);
            SaveChanges();
        }
    }
}