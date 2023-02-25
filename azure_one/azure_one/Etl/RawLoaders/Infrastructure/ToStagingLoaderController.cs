using System;
using azure_one.Etl.RawLoaders.Application;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class ToStagingLoaderController
{
    private readonly TruncateImpTablesService _truncateImpTablesService;
    private readonly LoadLanguagesRawService _loadLanguagesRawService;
    private readonly LoadCountriesRawServices _loadCountriesRawServices;
    
    public ToStagingLoaderController(
        TruncateImpTablesService truncateImpTablesService,
        LoadLanguagesRawService loadLanguagesRawService,
        LoadCountriesRawServices loadCountriesRawServices
    )
    {
        _loadLanguagesRawService = loadLanguagesRawService;
        _loadCountriesRawServices = loadCountriesRawServices;
        _truncateImpTablesService = truncateImpTablesService;
    }

    public void Invoke()
    {
        _truncateImpTablesService.Invoke();
        _loadLanguagesRawService.Invoke();
        _loadCountriesRawServices.Invoke();
    }
}