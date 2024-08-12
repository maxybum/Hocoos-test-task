using ConsoleApp.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Helpers;

public static class ServiceContainer {
  private static readonly ServiceCollection _serviceCollection = new();

  static ServiceContainer() {
    ConfigureServices();
  }

  public static ServiceProvider GetServiceProvider() {
    return _serviceCollection.BuildServiceProvider();
  }

  private static void ConfigureServices() {
    _serviceCollection.AddLogging(configure => configure.AddConsole());
    _serviceCollection.AddScoped<ExpressionBackendTask>();
    _serviceCollection.AddScoped<ThreadBackendTask>();
    _serviceCollection.AddScoped<ThreadsLimitedThreadBackendTask>();
    _serviceCollection.AddScoped<FieldsLimitedExpressionBackendTask>();
    _serviceCollection.AddScoped<FieldsObjectMapper>();
  }
}

