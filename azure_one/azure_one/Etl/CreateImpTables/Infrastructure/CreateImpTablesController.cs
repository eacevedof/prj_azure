using azure_one.Etl.InstallImpTables.Application;

namespace azure_one.Etl.CreateImpTables.Infrastructure;

public sealed class CreateImpTablesController
{
    private readonly CreateImpTablesService _createImpTablesService;

    public CreateImpTablesController(CreateImpTablesService createImpTablesService)
    {
        _createImpTablesService = createImpTablesService;
    }

    public void Invoke()
    {
        _createImpTablesService.Invoke();
    }
}