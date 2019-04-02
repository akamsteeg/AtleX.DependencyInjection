using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtleX.DependencyInjection.Modules
{
  /// <summary>
  /// Represents a registrar for a module for dependency injection
  /// </summary>
  public interface IModuleRegistrar
  {
    /// <summary>
    /// Configure the services for the module
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to register the services in
    /// </param>
    /// <param name="configuration">
    /// The <see cref="IConfiguration"/> to use
    /// </param>
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
  }
}
