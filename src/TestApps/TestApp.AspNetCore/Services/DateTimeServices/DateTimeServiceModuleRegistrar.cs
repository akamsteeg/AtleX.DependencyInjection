using AtleX.DependencyInjection.Modules.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestApp.AspNetCore.Services.DateTimeServices
{
  public class DateTimeServiceModuleRegistrar
    : IAspModuleRegistrar
  {
    public void Configure(IApplicationBuilder application, IHostingEnvironment environment, IConfiguration configuration)
    {
      var s = application.ApplicationServices.GetService<DateTimeService>();
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddTransient<DateTimeService>();
    }
  }
}
