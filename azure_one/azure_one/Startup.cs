using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using azure_one.Etl.Application;
using azure_one.Etl.Infrastructure.Db;
using azure_one.Etl.Infrastructure.Files;
using azure_one.Etl.Infrastructure.Repositories;


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
        builder.Services.AddSingleton<CreateUserService>(
            s => new CreateUserService(new UsersRepository(new Mssql()))
        );
        builder.Services.AddSingleton<LoadExcelService>(
            s => new LoadExcelService()
        );
        
        //fix: No data is available for encoding 1252. For information on defining a custom encoding
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }
}