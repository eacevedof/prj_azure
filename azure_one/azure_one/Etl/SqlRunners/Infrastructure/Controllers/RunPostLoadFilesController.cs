using azure_one.Etl.SqlRunners.Application;

namespace azure_one.Etl.SqlRunners.Infrastructure.Controllers;

public sealed class RunPostLoadFilesController
{
    private readonly PostLoadImpTablesService _postLoadImpTablesService;
    
    public RunPostLoadFilesController(PostLoadImpTablesService postLoadImpTablesService)
    {
        _postLoadImpTablesService = postLoadImpTablesService;
    }

    public void Invoke()
    {
        _postLoadImpTablesService.Invoke();
    }
}