using azure_one.Etl.CreateImpTables.Infrastructure.Repositories;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Global;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

namespace azure_one.Etl.InstallImpTables.Application;

public sealed class CreateImpTablesService
{
    private readonly CreateImpTablesRepository _createImpTablesRepository;

    public CreateImpTablesService()
    {
        ContextDto contextDto = ContextFinder.GetById(Req.ContextId);
        _createImpTablesRepository = new CreateImpTablesRepository(Mssql.GetInstanceByDto(contextDto));
    }
    
    public void Invoke()
    {
        _createImpTablesRepository.Invoke();
    }
}