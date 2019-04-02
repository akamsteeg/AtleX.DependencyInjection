using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtleX.DependencyInjection.Modules.Tests.Mocks
{
  public sealed class MockServiceModuleRegistrar
    : IModuleRegistrar
  {
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddTransient<MockService>();
    }
  }
}
