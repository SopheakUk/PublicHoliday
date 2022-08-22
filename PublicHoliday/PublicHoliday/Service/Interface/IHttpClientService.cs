namespace PublicHoliday.Service.Interface
{
    public interface IHttpClientService
    {
        Task<TResponse> Get<TResponse>(string url);
    }
}