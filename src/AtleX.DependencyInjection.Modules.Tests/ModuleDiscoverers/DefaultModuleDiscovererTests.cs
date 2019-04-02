using AtleX.DependencyInjection.Modules.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace AtleX.DependencyInjection.Modules.Tests.ModuleDiscoverers
{
  public class DefaultModuleDiscovererTests
  {
    [Fact]
    public void DiscoverModules_WithNullAssembly_ThrowsArgumentNullException()
    {
      var d = new DefaultModuleDiscoverer<IModuleRegistrar>();

      Assert.Throws<ArgumentNullException>(() => d.DiscoverModules((Assembly)null));
    }

    [Fact]
    public void DiscoverModules_WithAssembly_DoesNotThrowArgumentNullException()
    {
      var d = new DefaultModuleDiscoverer<IModuleRegistrar>();

      d.DiscoverModules(this.GetType().Assembly);
    }

    [Fact]
    public void DiscoverModules_WithAssembly_DiscoversModuleRegistrar()
    {
      var d = new DefaultModuleDiscoverer<IModuleRegistrar>();

      var modules = d.DiscoverModules(this.GetType().Assembly);

      Assert.Contains(modules, m => m is MockServiceModuleRegistrar);
    }

    [Fact]
    public void DiscoverModules_WithZeroAssemblies_DoesNotDiscoverModuleRegistrar()
    {
      var d = new DefaultModuleDiscoverer<IModuleRegistrar>();

      var modules = d.DiscoverModules();

      Assert.DoesNotContain(modules, m => m is MockServiceModuleRegistrar);
    }

    [Fact]
    public void DiscoverModules_WithMultipleAssembliesDiscoversModuleRegistrar()
    {
      var d = new DefaultModuleDiscoverer<IModuleRegistrar>();

      var modules = d.DiscoverModules(typeof(object).Assembly, this.GetType().Assembly);

      Assert.Contains(modules, m => m is MockServiceModuleRegistrar);
    }
  }
}
