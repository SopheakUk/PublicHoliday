using PublicHoliday.Model.Response;

namespace PublicHoliday.Model
{
    public class SupportedCountry : EntityBase
    {
        public SupportedCountry()
        {
        }

        public SupportedCountry(SupportedCountryResponse supportedCountryResponse)
        {
            FullName = supportedCountryResponse.FullName;
            FromMonth = supportedCountryResponse.FromDate.Month;
            FromDay = supportedCountryResponse.FromDate.Day;
            CountryCode = supportedCountryResponse.CountryCode;
            FromYear = supportedCountryResponse.FromDate.Year;
            HolidayTypes = supportedCountryResponse.HolidayTypes?.Select(p => new HolidayType(p)).ToList() ?? new List<HolidayType>();
            Regions = supportedCountryResponse.Regions?.Select(p => new Region(p)).ToList() ?? new List<Region>();
            ToDay = supportedCountryResponse.ToDate.Day;
            ToMonth = supportedCountryResponse.ToDate.Month;
            ToYear = supportedCountryResponse.ToDate.Year;
        }

        public string CountryCode { get; set; }

        public virtual List<Region> Regions { get; set; }

        public virtual List<HolidayType> HolidayTypes { get; set; }

        public string FullName { get; set; }

        public int FromDay { get; set; }

        public int FromMonth { get; set; }

        public int FromYear { get; set; }
        public int ToDay { get; set; }

        public int ToMonth { get; set; }

        public int ToYear { get; set; }
    }

    public class Region : EntityBase
    {
        public Region()
        {
        }

        public Region(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class HolidayType : EntityBase
    {
        public HolidayType()
        {
        }

        public HolidayType(string type)
        {
            Type = type;
        }

        public string Type { get; set; }
    }
}