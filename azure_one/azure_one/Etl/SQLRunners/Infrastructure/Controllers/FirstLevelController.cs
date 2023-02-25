using azure_one.Etl.SQLRunners.Application;

namespace azure_one.Etl.SQLRunners.Infrastructure.Controllers;

public sealed class FirstLevelController
{
    private readonly RunSqlFilesService _runSqlFilesService;
    
    public FirstLevelController(RunSqlFilesService runSqlFilesService)
    //public FirstLevelController()
    {
        _runSqlFilesService = runSqlFilesService;
    }

    public void Invoke()
    {
        _runSqlFilesService.Invoke();
    }
}