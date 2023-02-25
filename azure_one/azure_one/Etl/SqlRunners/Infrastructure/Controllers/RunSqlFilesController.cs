using azure_one.Etl.SqlRunners.Application;

namespace azure_one.Etl.SqlRunners.Infrastructure.Controllers;

public sealed class FirstLevelController
{
    private readonly RunSqlFilesService _runSqlFilesService;
    
    public FirstLevelController(RunSqlFilesService runSqlFilesService)
    {
        _runSqlFilesService = runSqlFilesService;
    }

    public void Invoke()
    {
        _runSqlFilesService.Invoke();
    }
}