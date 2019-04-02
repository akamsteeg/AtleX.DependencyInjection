using AtleX.DependencyInjection.Modules.Tests.Mocks;
using System;
using Xunit;

namespace AtleX.DependencyInjection.Modules.AspNetCore.Tests
{
  public class ModuleRegistrarOptionsTests
  {
    [Fact]
    public void AddAssembly_WithNullAssembly_ThrowsArgumentNullException()
    {
      var o = new ModuleRegistrarOptions();

      Assert.Throws<ArgumentNullException>(() => o.AddAssembly(null));
    }

    [Fact]
    public void AddAssembly_WithAssembly_DoesNotThrowArgumentNullException()
    {
      var o = new ModuleRegistrarOptions();

      o.AddAssembly(this.GetType().Assembly);
    }

    [Fact]
    public void AddAssembly_WithAssembly_DiscoversMockServiceModuleRegistrar()
    {
      var o = new ModuleRegistrarOptions();

      o.AddAssembly(this.GetType().Assembly);

      Assert.Contains(o.ModuleRegistrars, m => m is MockServiceModuleRegistrar);
    }

    [Fact]
    public void AddAssemblies_WithNoAssembly_DoesNotThrowArgumentNullException()
    {
      var o = new ModuleRegistrarOptions();

      o.AddAssemblies();
    }

    [Fact]
    public void AddAssemblies_WithAssemblies_DiscoversMockServiceModuleRegistrar()
    {
      var o = new ModuleRegistrarOptions();

      o.AddAssemblies(typeof(object).Assembly, this.GetType().Assembly);

      Assert.Contains(o.ModuleRegistrars, m => m is MockServiceModuleRegistrar);
    }
  }
}
