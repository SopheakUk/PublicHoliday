namespace PublicHoliday.Service.Interface
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> Get(string url);
    }
}