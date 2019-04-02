using Pitcher;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AtleX.DependencyInjection.Modules
{
  /// <summary>
  /// Represents a discoverer for <see cref="IModuleRegistrar"/>
  /// </summary>
  /// <typeparam name="TServiceType">
  /// the type of <see cref="IModuleRegistrar"/> to discover
  /// </typeparam>
  public sealed class DefaultModuleDiscoverer<TServiceType>
    where TServiceType: IModuleRegistrar
  {
    /// <summary>
    /// Discover the modules
    /// </summary>
    /// <param name="assembly">
    /// The <see cref="Assembly"/> to discover the modules in
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{T}"/> with the discovered modules
    /// </returns>
    public IEnumerable<TServiceType> DiscoverModules(Assembly assembly)
      => DiscoverModulesInternal(assembly);

    /// <summary>
    /// Discover the modules
    /// </summary>
    /// <param name="assemblies">
    /// The collection of <see cref="Assembly"/> to discover the modules in
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{T}"/> with the discovered modules
    /// </returns>
    public IEnumerable<TServiceType> DiscoverModules(params Assembly[] assemblies)
    {
      var result = new List<TServiceType>();

      foreach (var currentAssembly in assemblies)
      {
        var moduleRegistrars = DiscoverModulesInternal(currentAssembly);

        result.AddRange(moduleRegistrars);
      }

      return result;
    }

    /// <summary>
    /// Discover the modules
    /// </summary>
    /// <param name="assembly">
    /// The <see cref="Assembly"/> to discover the modules in
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable{T}"/> with the discovered modules
    /// </returns>
    private static IEnumerable<TServiceType> DiscoverModulesInternal(Assembly assembly)
    {
      Throw.ArgumentNull.WhenNull(assembly, nameof(assembly));

      var result = new List<TServiceType>();

      var exportedTypes = assembly.GetExportedTypes();

      var moduleRegistrarType = typeof(IModuleRegistrar);

      foreach (var currentExportedType in exportedTypes)
      {
        if (moduleRegistrarType.IsAssignableFrom(currentExportedType))
        {
          var moduleRegistrar = CreateInstance(currentExportedType);

          result.Add(moduleRegistrar);
        }
      }

      return result;
    }

    /// <summary>
    /// Create an instance of the specified module registrar
    /// </summary>
    /// <param name="moduleRegistrarToInstantiate">
    /// The module registrar to instantiate
    /// </param>
    /// <returns>
    /// A new instance of the module registrar
    /// </returns>
    private static TServiceType CreateInstance(Type moduleRegistrarToInstantiate)
    {
      Throw.ArgumentNull.WhenNull(moduleRegistrarToInstantiate, nameof(moduleRegistrarToInstantiate));

      var result = (TServiceType)Activator.CreateInstance(moduleRegistrarToInstantiate);

      return result;
    }
  }
}
