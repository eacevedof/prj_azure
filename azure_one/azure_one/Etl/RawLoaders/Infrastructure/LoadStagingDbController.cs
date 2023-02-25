using azure_one.Etl.RawLoaders.Application;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadStagingDbController
{
    private readonly TruncateImpTablesService _truncateImpTablesService;
    private readonly LoadXlsLanguagesService _loadXlsLanguagesService;
    private readonly LoadXlsCountriesServices _loadXlsCountriesServices;
    private readonly LoadXlsProvincesService _loadXlsProvincesService;
    
    private readonly LoadXlsCitiesService _loadXlsCitiesService;
    private readonly LoadXlsCompaniesService _loadXlsCompaniesService;
    
    public LoadStagingDbController(
        TruncateImpTablesService truncateImpTablesService,
        LoadXlsLanguagesService loadXlsLanguagesService,
        LoadXlsCountriesServices loadXlsCountriesServices,
        LoadXlsProvincesService loadXlsProvincesService,
        LoadXlsCitiesService loadXlsCitiesService,
        LoadXlsCompaniesService loadXlsCompaniesService
    )
    {
        _loadXlsLanguagesService = loadXlsLanguagesService;
        _loadXlsCountriesServices = loadXlsCountriesServices;
        _truncateImpTablesService = truncateImpTablesService;
        _loadXlsProvincesService = loadXlsProvincesService;
        _loadXlsCitiesService = loadXlsCitiesService;
        _loadXlsCompaniesService = loadXlsCompaniesService;
    }

    public void Invoke()
    {
        _truncateImpTablesService.Invoke();
        _loadXlsLanguagesService.Invoke();
        _loadXlsCountriesServices.Invoke();
        _loadXlsProvincesService.Invoke();
    }
}