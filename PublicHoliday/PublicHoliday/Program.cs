using Microsoft.OpenApi.Models;
using PublicHoliday.Service.Interface;
using PublicHoliday.Service;
using PublicHoliday.Repository.Interface;
using PublicHoliday.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PublicHoliday.Filter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<IHttpClientService, HttpClientService>().SetHandlerLifetime(TimeSpan.FromSeconds(5));
builder.Services.AddSingleton<IPublicHolidayService, PublicHolidayService>();
builder.Services.AddSingleton<IPublicHolidayRepository, PublicHolidayRepository>();
builder.Services.TryAddSingleton<ExceptionFilter>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Public Holiday API",
        Version = "v1",
        Description = "Public Holiday Document"
    });
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IPublicHolidayRepository>() as PublicHolidayRepository;
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();