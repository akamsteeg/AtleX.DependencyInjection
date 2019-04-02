using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pitcher;
using System.Reflection;

namespace AtleX.DependencyInjection.Modules
{
  /// <summary>
  /// Extension methods for <see cref="IServiceCollection"/>
  /// </summary>
  public static class IServiceCollectionExtensions
  {
    /// <summary>
    /// Register the modules
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to register the modules in
    /// </param>
    /// <param name="configuration">
    /// The <see cref="IConfiguration"/> to use
    /// </param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with the modules registered
    /// </returns>
    public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration)
    {
      Throw.ArgumentNull.WhenNull(services, nameof(services));
      Throw.ArgumentNull.WhenNull(configuration, nameof(configuration));

      services.RegisterModulesInternal(configuration, Assembly.GetEntryAssembly());

      return services;
    }

    /// <summary>
    /// Register the modules
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to register the modules in
    /// </param>
    /// <param name="configuration">
    /// The <see cref="IConfiguration"/> to use
    /// </param>
    /// <param name="assembly">
    /// The <see cref="Assembly"/> to discover the modules in
    /// </param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with the modules registered
    /// </returns>
    public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
      Throw.ArgumentNull.WhenNull(services, nameof(services));
      Throw.ArgumentNull.WhenNull(configuration, nameof(configuration));
      Throw.ArgumentNull.WhenNull(assembly, nameof(assembly));

      services.RegisterModulesInternal(configuration, assembly);

      return services;
    }

    /// <summary>
    /// Register the modules
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to register the modules in
    /// </param>
    /// <param name="configuration">
    /// The <see cref="IConfiguration"/> to use
    /// </param>
    /// <param name="assembly">
    /// The <see cref="Assembly"/> to discover the modules in
    /// </param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with the modules registered
    /// </returns>
    private static IServiceCollection RegisterModulesInternal(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
      Throw.ArgumentNull.WhenNull(services, nameof(services));
      Throw.ArgumentNull.WhenNull(configuration, nameof(configuration));
      Throw.ArgumentNull.WhenNull(assembly, nameof(assembly));

      var moduleDiscoverer = new DefaultModuleDiscoverer<IModuleRegistrar>();
      var moduleRegistrars = moduleDiscoverer.DiscoverModules(assembly);

      foreach (var currentModuleRegistrar in moduleRegistrars)
      {
        currentModuleRegistrar.ConfigureServices(services, configuration);
      }

      return services;
    }
  }
}
