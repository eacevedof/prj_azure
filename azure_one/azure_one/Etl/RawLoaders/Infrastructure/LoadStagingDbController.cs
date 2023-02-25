using azure_one.Etl.RawLoaders.Application;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadStagingDbController
{
    private readonly TruncateImpTablesService _truncateImpTablesService;
    private readonly LoadXlsLanguagesService _loadXlsLanguagesService;
    private readonly LoadXlsCountriesServices _loadXlsCountriesServices;
    private readonly LoadXlsProvincesService _loadXlsProvincesService;
    
    public LoadStagingDbController(
        TruncateImpTablesService truncateImpTablesService,
        LoadXlsLanguagesService loadXlsLanguagesService,
        LoadXlsCountriesServices loadXlsCountriesServices,
        LoadXlsProvincesService loadXlsProvincesService
    )
    {
        _loadXlsLanguagesService = loadXlsLanguagesService;
        _loadXlsCountriesServices = loadXlsCountriesServices;
        _truncateImpTablesService = truncateImpTablesService;
        _loadXlsProvincesService = loadXlsProvincesService;
    }

    public void Invoke()
    {
        _truncateImpTablesService.Invoke();
        _loadXlsLanguagesService.Invoke();
        _loadXlsCountriesServices.Invoke();
        _loadXlsProvincesService.Invoke();
    }
}