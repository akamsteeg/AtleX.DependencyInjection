# AtleX.DependencyInjection

AtleX.DependencyInjection is an extension to Microsoft's dependency injection in (ASP).NET Core
to enable module-like registration of services. This keeps your service registration, and especially
the `Startup` class in ASP.net Core, sane.

## Installation

AtleX.DependencyInjection is available in two NuGet packages.

For ASP.net Core:

```powershell
dotnet add project.csproj package [AtleX.DependencyInjection.Modules.AspNetCore](https://www.nuget.org/packages/AtleX.DependencyInjection.Modules.AspNetCore)
```

Generic .NET Core:

```powershell
dotnet add project.csproj package [AtleX.DependencyInjection.Modules](https://www.nuget.org/packages/AtleX.DependencyInjection.Modules)
```


## Usage

### ASP.net Core

Instead of filling up the `Startup` class with lots of service registrations and configurations, the services
are configured as autodiscovered modules.

Consider the following:

(Startup.cs)
```csharp
public class Startup
{
  // This method gets called by the runtime. Use this method to add services to the container.
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddMvc()
      .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
      .AddJsonOptions(options =>
      {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
      });

    services.AddScoped<IDataProvider>(CreateDataProvider);
  }

  private IDataProvider CreateDataProvider(IServiceProvider serviceProvider)
  {
    var result = new DefaultDataProvider();

    return result;
  }
}
```

When registering multiple services, the `Startup` class quickly fills up with lots of service registrations, factory
methods, configuration, etc. AtleX.DependencyInjection.Modules.AspNetCore aims to replace that with autodiscovered 
modules.

To register the data provider from the previous example, create a `DataProviderAspModuleRegistrar` that implements 
`IAspModuleRegistrar`:

```csharp
public class DataProviderAspModuleRegistrar : IAspModuleRegistrar
{
  public void Configure(IApplicationBuilder application, IHostingEnvironment environment, IConfiguration configuration)
  {
      
  }

  public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IDataProvider>(CreateDataProvider);
  }

  private IDataProvider CreateDataProvider(IServiceProvider serviceProvider)
  {
    var result = new DefaultDataProvider();

    return result;
  }
}
```

In `Program.cs`, add `UseModuleRegistrars()` to the `IWebHostBuilder` created 
in `CreateWebHostBuilder(string[])`. This enables the autodiscovery of the modules:

```csharp
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
  WebHost.CreateDefaultBuilder(args)
    .UseModuleRegistrars()
    .UseStartup<Startup>();
```

You'll notice the familiar `ConfigureServices()` and `Configure()` methods when 
implementing `IAspModuleRegistrar`. These methods work the same as the ones in 
`Startup` and provide ways to register and configure services in the application.

By default the modules are discovered in the entry assembly of the application. By 
configuring the options of `UseModuleRegistrars()` you can add additional assemblies 
to scan:

```csharp
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
  WebHost.CreateDefaultBuilder(args)
    .UseModuleRegistrars(options =>
    {
      options.AddAssembly(typeof(Service1).Assembly);
      options.AddAssembly(typeof(Service2).Assembly);
    })
    .UseStartup<Startup>();
```

### Generic .NET Core dependency injection

To register modules in the generic (non-ASP) .NET Core dependency injection, create a module registrar:

```csharp
public class DataProviderAspModuleRegistrar : IModuleRegistrar
{
  public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IDataProvider>(CreateDataProvider);
  }

  private IDataProvider CreateDataProvider(IServiceProvider serviceProvider)
  {
    var result = new DefaultDataProvider();

    return result;
  }
}
```

When configuring the services, call `RegisterModules(IConfiguration)` on the `IServiceCollection`:

```csharp

// Register modules from the entry assembly:
services.RegisterModules(this.Configuration);

// Also register modules from another assembly:
services.RegisterModules(this.Configuration, typeof(Service1).Assembly);
```

License

AtleX.DependencyInjection uses the MIT license, see the LICENSE file.