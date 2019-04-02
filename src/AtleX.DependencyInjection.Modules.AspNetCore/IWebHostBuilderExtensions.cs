using AtleX.DependencyInjection.Modules.AspNetCore.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Pitcher;
using System;
using System.Reflection;

namespace AtleX.DependencyInjection.Modules.AspNetCore
{
  /// <summary>
  /// Extension methods for <see cref="IWebHostBuilder"/>
  /// </summary>
  public static class IWebHostBuilderExtensions
  {
    /// <summary>
    /// Use the registered modules
    /// </summary>
    /// <param name="builder">
    /// The <see cref="IWebHostBuilder"/> to register the modules in
    /// </param>
    /// <returns>
    /// The <see cref="IWebHostBuilder"/> with the configured modules
    /// </returns>
    public static IWebHostBuilder UseModuleRegistrars(this IWebHostBuilder builder)
      => builder.UseModuleRegistrars(_ => { });

    /// <summary>
    /// Use the registered modules
    /// </summary>
    /// <param name="builder">
    /// The <see cref="IWebHostBuilder"/> to register the modules in
    /// </param>
    /// <param name="configure">
    /// The <see cref="Action{T}"/> of <see cref="ModuleRegistrarOptions"/>
    /// </param>
    /// <returns>
    /// The <see cref="IWebHostBuilder"/> with the configured modules
    /// </returns>
    public static IWebHostBuilder UseModuleRegistrars(this IWebHostBuilder builder, Action<ModuleRegistrarOptions> configure)
    {
      Throw.ArgumentNull.WhenNull(builder, nameof(builder));
      Throw.ArgumentNull.WhenNull(configure, nameof(configure));

      var options = new ModuleRegistrarOptions();
      configure(options);

      if (options.AutoScanEntryAssembly)
      {
        options.AddAssembly(Assembly.GetEntryAssembly());
      }

      builder.ConfigureServices((builderContext, services) =>
      {
        services.AddSingleton<IStartupFilter>(sp => ActivatorUtilities.CreateInstance<AspModuleRegistrarStartupFilter>(sp, options));

        foreach (var currentModuleRegistrar in options.ModuleRegistrars)
        {
          currentModuleRegistrar.ConfigureServices(services, builderContext.Configuration);
        }
      });

      return builder;
    }
  }
}
