using AtleX.DependencyInjection.Modules.Tests.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace AtleX.DependencyInjection.Modules.Tests
{
  public class IServiceProviderExtensionsTests
  {
    [Fact]
    public void RegisterModules_WithNullConfiguration_ThrowsArgumentNullException()
    {
      var services = new Mock<IServiceCollection>().Object;

      Assert.Throws<ArgumentNullException>(() => services.RegisterModules(null));
    }

    [Fact]
    public void RegisterModules_WithConfiguration_DoesNotThrowArgumentNullException()
    {
      var services = new Mock<IServiceCollection>().Object;
      var configuration = new Mock<IConfiguration>().Object;

      services.RegisterModules(configuration);
    }

    [Fact]
    public void RegisterModules_WithConfigurationAndNullAssembly_ThrowsArgumentNullException()
    {
      var services = new Mock<IServiceCollection>().Object;
      var configuration = new Mock<IConfiguration>().Object;

      Assert.Throws<ArgumentNullException>(() => services.RegisterModules(configuration, null));
    }

    [Fact]
    public void RegisterModules_WithConfigurationAndAssembly_DoesNotThrowArgumentNullException()
    {
      var services = new Mock<IServiceCollection>().Object;
      var configuration = new Mock<IConfiguration>().Object;

      services.RegisterModules(configuration, this.GetType().Assembly);
    }

    [Fact]
    public void RegisterModules_WithConfigurationAndAssembly_RegistersService()
    {
      var services = new ServiceCollection();
      var configuration = new Mock<IConfiguration>().Object;

      services.RegisterModules(configuration, this.GetType().Assembly);

      Assert.NotNull(services.BuildServiceProvider().GetService<MockService>());
    }
  }
}
