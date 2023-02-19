using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly:FunctionsStartup(typeof(azure_one.Startup))]
namespace azure_one;

public class Startup: FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        //aqui se configura la inyeccion de dependecias
    }
}