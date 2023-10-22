
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

using Fn.Users.Models;
using Fn.Users.Services;
using Fn.Users.Controllers;

[assembly:FunctionsStartup(typeof(azurefn_mvc.Startup))]
namespace azurefn_mvc;

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
        //builder.Services.AddSingleton<UsersRepository>(s => new UsersRepository());
        
        //services
        builder.Services.AddSingleton<GetUsersService>(s => new GetUsersService(new UsersRepository()));
       
        //controllers
        builder.Services.AddSingleton<GetUsersController>(
            s => new GetUsersController(
                new GetUsersService(
                    new UsersRepository()
                )
            )
        );

        //fix: No data is available for encoding 1252. For information on defining a custom encoding
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }
}