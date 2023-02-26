using azure_one.Etl.RawLoaders.Application;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadStagingDbController
{
    private readonly PreLoadImpTablesService _preLoadImpTablesService;
    private readonly LoadXlsLanguagesService _loadXlsLanguagesService;
    private readonly LoadXlsCountriesServices _loadXlsCountriesServices;
    private readonly LoadXlsProvincesService _loadXlsProvincesService;
    
    private readonly LoadXlsCitiesService _loadXlsCitiesService;
    private readonly LoadXlsCompaniesService _loadXlsCompaniesService;
    
    public LoadStagingDbController(
        PreLoadImpTablesService preLoadImpTablesService,
        LoadXlsLanguagesService loadXlsLanguagesService,
        LoadXlsCountriesServices loadXlsCountriesServices,
        LoadXlsProvincesService loadXlsProvincesService,
        LoadXlsCitiesService loadXlsCitiesService,
        LoadXlsCompaniesService loadXlsCompaniesService
    )
    {
        _loadXlsLanguagesService = loadXlsLanguagesService;
        _loadXlsCountriesServices = loadXlsCountriesServices;
        _preLoadImpTablesService = preLoadImpTablesService;
        _loadXlsProvincesService = loadXlsProvincesService;
        _loadXlsCitiesService = loadXlsCitiesService;
        _loadXlsCompaniesService = loadXlsCompaniesService;
    }

    public void Invoke()
    {
        _preLoadImpTablesService.Invoke();
        _loadXlsLanguagesService.Invoke();
        _loadXlsCountriesServices.Invoke();
        _loadXlsProvincesService.Invoke();
        _loadXlsCitiesService.Invoke();
        _loadXlsCompaniesService.Invoke();
    }
}