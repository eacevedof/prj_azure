using azure_one.Etl.RawLoaders.Application;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadStagingDbController
{
    private readonly LoadXlsLanguagesService _loadXlsLanguagesService;
    private readonly LoadXlsCountriesServices _loadXlsCountriesServices;
    private readonly LoadXlsProvincesService _loadXlsProvincesService;
    private readonly LoadXlsCitiesService _loadXlsCitiesService;
    private readonly LoadXlsCompaniesService _loadXlsCompaniesService;
    private readonly LoadXlsLanguagesCompanyCustomService _loadXlsLanguagesCompanyCustomService;
    private readonly LoadXlsUserTypesService _loadXlsUserTypesService;
    private readonly LoadXlsStatusEmployeesService _loadXlsStatusEmployeesService;
    private readonly LoadXlsEmployeesPositionsService _loadXlsEmployeesPositionsService;
    private readonly LoadXlsEmployeesDepartmentsService _loadXlsEmployeesDepartmentsService;
    private readonly LoadXlsRolesService _loadXlsRolesService;
    private readonly LoadXlsEmployeesService _loadXlsEmployeesService;
    
    public LoadStagingDbController(
        LoadXlsLanguagesService loadXlsLanguagesService,
        LoadXlsCountriesServices loadXlsCountriesServices,
        LoadXlsProvincesService loadXlsProvincesService,
        LoadXlsCitiesService loadXlsCitiesService,
        LoadXlsCompaniesService loadXlsCompaniesService,
        LoadXlsLanguagesCompanyCustomService loadXlsLanguagesCompanyCustomService,
        LoadXlsUserTypesService loadXlsUserTypesService,
        LoadXlsStatusEmployeesService loadXlsStatusEmployeesService,
        LoadXlsEmployeesPositionsService loadXlsEmployeesPositionsService,
        LoadXlsEmployeesDepartmentsService loadXlsEmployeesDepartmentsService,
        LoadXlsRolesService loadXlsRolesService,
        LoadXlsEmployeesService loadXlsEmployeesService
    )
    {
        _loadXlsLanguagesService = loadXlsLanguagesService;
        _loadXlsCountriesServices = loadXlsCountriesServices;
        _loadXlsProvincesService = loadXlsProvincesService;
        _loadXlsCitiesService = loadXlsCitiesService;
        _loadXlsCompaniesService = loadXlsCompaniesService;
        _loadXlsLanguagesCompanyCustomService = loadXlsLanguagesCompanyCustomService;
        _loadXlsUserTypesService = loadXlsUserTypesService;
        _loadXlsStatusEmployeesService = loadXlsStatusEmployeesService;
        _loadXlsEmployeesPositionsService = loadXlsEmployeesPositionsService;
        _loadXlsEmployeesDepartmentsService = loadXlsEmployeesDepartmentsService;
        _loadXlsRolesService = loadXlsRolesService;
        _loadXlsEmployeesService = loadXlsEmployeesService;
    }

    public void Invoke()
    {
        _loadXlsCitiesService.Invoke();
/*        
        _loadXlsLanguagesService.Invoke();
        _loadXlsCountriesServices.Invoke();
        _loadXlsProvincesService.Invoke();
        _loadXlsCitiesService.Invoke();
        _loadXlsCompaniesService.Invoke();
        _loadXlsLanguagesCompanyCustomService.Invoke();
        _loadXlsUserTypesService.Invoke();
        _loadXlsStatusEmployeesService.Invoke();
        _loadXlsEmployeesPositionsService.Invoke();
        _loadXlsEmployeesDepartmentsService.Invoke();
        _loadXlsRolesService.Invoke();
        */
        //_loadXlsEmployeesService.Invoke();
    }
}