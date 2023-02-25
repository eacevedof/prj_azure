using azure_one.Etl.SqlRunners.Application;

namespace azure_one.Etl.SqlRunners.Infrastructure.Controllers;

public sealed class RunSqlFilesController
{
    private readonly RunSqlFilesService _runSqlFilesService;
    
    public RunSqlFilesController(RunSqlFilesService runSqlFilesService)
    {
        _runSqlFilesService = runSqlFilesService;
    }

    public void Invoke()
    {
        _runSqlFilesService.Invoke();
    }
}