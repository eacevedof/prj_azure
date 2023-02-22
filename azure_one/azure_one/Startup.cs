using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Repositories;
using azure_one.Etl.RawLoaders.Application;
using azure_one.Etl.RawLoaders.Infrastructure;


[assembly:FunctionsStartup(typeof(azure_one.Startup))]
namespace azure_one;

public class Startup: FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);
        builder.ConfigurationBuilder.SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("settings-file.json", true);
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        //aqui se configura la inyeccion de dependecias
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<TruncateRepository>(
            s => new TruncateRepository(new Mssql())
        );
        builder.Services.AddSingleton<LoadLanguagesRawService>(
            s => new LoadLanguagesRawService()
        );
        builder.Services.AddSingleton<LoadCountriesRawServices>(
            s => new LoadCountriesRawServices()
        );
        builder.Services.AddSingleton<RawLoadersController>(
            s => new RawLoadersController(new LoadLanguagesRawService(), new LoadCountriesRawServices())
        );
            
        //fix: No data is available for encoding 1252. For information on defining a custom encoding
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }
}