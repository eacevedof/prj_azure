using System;
using azure_one.Etl.RawLoaders.Application;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class RawLoadersController
{
    private readonly TruncateTableService _truncateTableService;
    private readonly LoadLanguagesRawService _loadLanguagesRawService;
    private readonly LoadCountriesRawServices _loadCountriesRawServices;
    
    public RawLoadersController(
        TruncateTableService truncateTableService,
        LoadLanguagesRawService loadLanguagesRawService,
        LoadCountriesRawServices loadCountriesRawServices
    )
    {
        _loadLanguagesRawService = loadLanguagesRawService;
        _loadCountriesRawServices = loadCountriesRawServices;
        _truncateTableService = truncateTableService;
    }

    public void Invoke()
    {
        _truncateTableService.Invoke();
        _loadLanguagesRawService.Invoke();
        _loadCountriesRawServices.Invoke();
    }
}