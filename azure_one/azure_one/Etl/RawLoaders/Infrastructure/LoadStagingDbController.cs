using azure_one.Etl.RawLoaders.Application;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadStagingDbController
{
    private readonly LoadXlsLanguagesService _loadXlsLanguagesService;
    private readonly LoadXlsCountriesServices _loadXlsCountriesServices;
    private readonly LoadXlsProvincesService _loadXlsProvincesService;
    
    private readonly LoadXlsCitiesService _loadXlsCitiesService;
    private readonly LoadXlsCompaniesService _loadXlsCompaniesService;
    private readonly LoadXlsLanguagesCompanyService _loadXlsLanguagesCompanyService;
    
    public LoadStagingDbController(
        LoadXlsLanguagesService loadXlsLanguagesService,
        LoadXlsCountriesServices loadXlsCountriesServices,
        LoadXlsProvincesService loadXlsProvincesService,
        LoadXlsCitiesService loadXlsCitiesService,
        LoadXlsCompaniesService loadXlsCompaniesService,
        LoadXlsLanguagesCompanyService loadXlsLanguagesCompanyService
    )
    {
        _loadXlsLanguagesService = loadXlsLanguagesService;
        _loadXlsCountriesServices = loadXlsCountriesServices;
        _loadXlsProvincesService = loadXlsProvincesService;
        _loadXlsCitiesService = loadXlsCitiesService;
        _loadXlsCompaniesService = loadXlsCompaniesService;
        _loadXlsLanguagesCompanyService = loadXlsLanguagesCompanyService;
    }

    public void Invoke()
    {
        _loadXlsLanguagesService.Invoke();
        _loadXlsCountriesServices.Invoke();
        _loadXlsProvincesService.Invoke();
        _loadXlsCitiesService.Invoke();
        _loadXlsCompaniesService.Invoke();
        _loadXlsLanguagesCompanyService.Invoke();
    }
}