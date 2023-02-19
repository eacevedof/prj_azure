using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using azure_one.Etl.Application;
using azure_one.Etl.Infrastructure.Db;
using azure_one.Etl.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
    }
}