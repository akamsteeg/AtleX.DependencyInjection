using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace AtleX.DependencyInjection.Modules.AspNetCore.Filters
{
  /// <summary>
  /// Represents an <see cref="IStartupFilter"/> that configures every
  /// <see cref="IAspModuleRegistrar"/>
  /// </summary>
  internal sealed class AspModuleRegistrarStartupFilter
    : IStartupFilter
  {
    /// <summary>
    /// Gets the <see cref="ModuleRegistrarOptions"/>
    /// </summary>
    private readonly ModuleRegistrarOptions _options;

    /// <summary>
    /// Gets the <see cref="IConfiguration"/> of the application
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Gets the <see cref="IHostingEnvironment"/> this application runs in
    /// </summary>
    private readonly IHostingEnvironment _hostingEnvironment;

    /// <summary>
    /// Initializes a new instance of <see cref="AspModuleRegistrarStartupFilter"/>
    /// </summary>
    /// <param name="serviceProvider">
    /// The <see cref="IServiceProvider"/> with registered services
    /// </param>
    /// <param name="options">
    /// The <see cref="ModuleRegistrarOptions"/>
    /// </param>
    /// <param name="configuration">
    /// The <see cref="IConfiguration"/> of the application
    /// </param>
    /// <param name="hostingEnvironment">
    /// The <see cref="IHostingEnvironment"/> this application runs in
    /// </param>
    public AspModuleRegistrarStartupFilter(IServiceProvider serviceProvider,
      ModuleRegistrarOptions options,
      IConfiguration configuration,
      IHostingEnvironment hostingEnvironment)
    {
      _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      this._options = options ?? throw new ArgumentNullException(nameof(options));
      this._hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
      this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Configure the <see cref="IApplicationBuilder"/>
    /// </summary>
    /// <param name="next">
    /// The next <see cref="Action{T}"/> of <see cref="IApplicationBuilder"/>
    /// </param>
    /// <returns>
    /// The <see cref="Action{T}"/> with the configured <see cref="IApplicationBuilder"/>
    /// </returns>
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
      void result(IApplicationBuilder builder)
      {
        foreach (var moduleRegistrar in this._options.ModuleRegistrars)
        {
          if (moduleRegistrar is IAspModuleRegistrar aspModuleRegistrar)
          {
            aspModuleRegistrar.Configure(builder, this._hostingEnvironment, this._configuration);
          }
        }

        next(builder);
      }

      return result;
    }
  }
}
