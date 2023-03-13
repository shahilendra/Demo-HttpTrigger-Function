using Demo.HttpTrigger.Services;
using Demo.HttpTrigger.Services.Abstraction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Demo.HttpTrigger.Function.Startup))]
namespace Demo.HttpTrigger.Function
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(_builder =>
            {
                _builder.SetMinimumLevel(LogLevel.Information);
                _builder.AddFilter("Demo.HttpTrigger.Function", LogLevel.Information);
                _builder.AddFilter("Demo.HttpTrigger.Services", LogLevel.Information);
            });
            builder.Services.AddSingleton<IDataService, DataService>();
        }
    }

}
