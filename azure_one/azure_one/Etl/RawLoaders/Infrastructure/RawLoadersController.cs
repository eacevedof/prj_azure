using System;
using azure_one.Etl.RawLoaders.Application;


namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class RawLoadersController
{
    private readonly LoadLanguagesRawService _loadLanguagesRawService;
    private readonly LoadCountriesRawServices _loadCountriesRawServices;
    
    public RawLoadersController(
        LoadLanguagesRawService loadLanguagesRawService,
        LoadCountriesRawServices loadCountriesRawServices
    )
    {
        _loadLanguagesRawService = loadLanguagesRawService;
        _loadCountriesRawServices = loadCountriesRawServices;
    }

    public void Invoke()
    {
        try
        {
            _loadLanguagesRawService.Invoke();
            _loadCountriesRawServices.Invoke();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}