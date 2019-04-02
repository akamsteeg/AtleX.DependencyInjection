using AtleX.DependencyInjection.Modules.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestApp.AspNetCore.Services
{
  public class EnableHttpsRedirectionModuleRegistrar
    : IAspModuleRegistrar
  {
    public void Configure(IApplicationBuilder application, IHostingEnvironment environment, IConfiguration configuration)
    {
      application.UseHttpsRedirection();
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }
  }
}
