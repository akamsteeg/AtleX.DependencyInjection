using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AtleX.DependencyInjection.Modules.AspNetCore
{
  /// <summary>
  /// Represents an <see cref="IModuleRegistrar"/> for ASP.net Core modules
  /// </summary>
  public interface IAspModuleRegistrar
    : IModuleRegistrar
  {
    /// <summary>
    /// Configure the HTTP request pipeline
    /// </summary>
    /// <param name="application">
    /// The <see cref="IApplicationBuilder"/> to configure the module in
    /// </param>
    /// <param name="environment"></param>
    /// <param name="configuration"></param>
    void Configure(IApplicationBuilder application, IHostingEnvironment environment, IConfiguration configuration);
  }
}
