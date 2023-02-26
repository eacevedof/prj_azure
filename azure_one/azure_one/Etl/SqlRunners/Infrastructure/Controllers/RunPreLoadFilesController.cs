using azure_one.Etl.SqlRunners.Application;

namespace azure_one.Etl.SqlRunners.Infrastructure.Controllers;

public sealed class RunPreLoadFilesController
{
    private readonly PreLoadImpTablesService _preLoadImpTablesService;
    
    public RunPreLoadFilesController(PreLoadImpTablesService preLoadImpTablesService)
    {
        _preLoadImpTablesService = preLoadImpTablesService;
    }

    public void Invoke()
    {
        _preLoadImpTablesService.Invoke();
    }
}