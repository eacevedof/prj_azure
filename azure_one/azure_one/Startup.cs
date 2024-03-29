using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using azure_one.Etl.Shared.Infrastructure.Db;

using azure_one.Etl.CreateImpTables.Infrastructure;

using azure_one.Etl.InstallImpTables.Application;

using azure_one.Etl.RawLoaders.Application.Force;
using azure_one.Etl.RawLoaders.Application.ImpTables;
using azure_one.Etl.RawLoaders.Infrastructure;

using azure_one.Etl.SqlRunners.Application;
using azure_one.Etl.SqlRunners.Infrastructure.Repositories;
using azure_one.Etl.SqlRunners.Infrastructure.Controllers;

using azure_one.Etl.HaveALook.Application;
using azure_one.Etl.HaveALook.Infrastructure.Repositories;


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
        
        //repositories
        builder.Services.AddSingleton<PreLoadRepository>(s => new PreLoadRepository(new Mssql()));
        
        //services
        builder.Services.AddSingleton<PreLoadImpTablesService>(s => new PreLoadImpTablesService(new PreLoadRepository(new Mssql())));
       
        //controlleres
        builder.Services.AddSingleton<LoadImpTablesController>(
            s => new LoadImpTablesController(
                new LoadXlsFilesIntoImpTables()
            )
        );

        builder.Services.AddSingleton<CreateImpTablesController>(
            s => new CreateImpTablesController(
                new CreateImpTablesService()
            )
        );        
        
        builder.Services.AddSingleton<LoadForceTableController>(
            s => new LoadForceTableController(
                new LoadXlsForceService()
            )
        );
        
        builder.Services.AddSingleton<RunPreLoadFilesController>(
            s => new RunPreLoadFilesController(new PreLoadImpTablesService(new PreLoadRepository(new Mssql())))
        );        
        builder.Services.AddSingleton<RunPostLoadFilesController>(
            s => new RunPostLoadFilesController(new PostLoadImpTablesService(new PostLoadRepository(new Mssql())))
        );

        builder.Services.AddSingleton<CheckPaginationService>(
            s => new CheckPaginationService()
        );
            
        //fix: No data is available for encoding 1252. For information on defining a custom encoding
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }
}