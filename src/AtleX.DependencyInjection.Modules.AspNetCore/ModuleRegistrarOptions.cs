using Pitcher;
using System.Collections.Generic;
using System.Reflection;

namespace AtleX.DependencyInjection.Modules.AspNetCore
{
  /// <summary>
  /// Represents the options for module registration
  /// </summary>
  public sealed class ModuleRegistrarOptions
  {
    /// <summary>
    /// Gets the <see cref="DefaultModuleDiscoverer{TServiceType}"/> or <see cref="IModuleRegistrar"/>
    /// </summary>
    private readonly DefaultModuleDiscoverer<IModuleRegistrar> _discoverer;

    /// <summary>
    /// Gets the <see cref="List{T}"/> or <see cref="IModuleRegistrar"/>
    /// </summary>
    private readonly List<IModuleRegistrar> _moduleRegistrars;

    /// <summary>
    /// Gets the <see cref="ICollection{T}"/> of <see cref="IModuleRegistrar"/>
    /// </summary>
    public ICollection<IModuleRegistrar> ModuleRegistrars => this._moduleRegistrars;

    /// <summary>
    /// Gets or sets whether to scan the entry assembly for module registrars
    /// </summary>
    public bool AutoScanEntryAssembly { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="ModuleRegistrarOptions"/>
    /// </summary>
    public ModuleRegistrarOptions()
    {
      this._discoverer = new DefaultModuleDiscoverer<IModuleRegistrar>();

      this._moduleRegistrars = new List<IModuleRegistrar>();

      this.AutoScanEntryAssembly = true;
    }

    /// <summary>
    /// Add the modules from an <see cref="Assembly"/>
    /// </summary>
    /// <param name="assemblyWithModules">
    /// The <see cref="Assembly"/> to add the modules from
    /// </param>
    public void AddAssembly(Assembly assemblyWithModules)
    {
      Throw.ArgumentNull.WhenNull(assemblyWithModules, nameof(assemblyWithModules));

      var discoveredModules = this._discoverer.DiscoverModules(assemblyWithModules);

      this._moduleRegistrars.AddRange(discoveredModules);
    }

    /// <summary>
    /// Add the modules from every <see cref="Assembly"/>
    /// </summary>
    /// <param name="assembliesWithModules">
    /// The assemblies to add the modules from
    /// </param>
    public void AddAssemblies(params Assembly[] assembliesWithModules)
    {
      var discoveredModules = this._discoverer.DiscoverModules(assembliesWithModules);

      this._moduleRegistrars.AddRange(discoveredModules);
    }
  }
}
