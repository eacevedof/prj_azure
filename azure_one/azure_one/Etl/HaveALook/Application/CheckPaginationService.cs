using azure_one.Etl.HaveALook.Infrastructure.Repositories;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Global;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

namespace azure_one.Etl.HaveALook.Application;

public sealed class CheckPaginationService
{
    private readonly GetAnyListRepository _getAnyListRepository;

    public CheckPaginationService()
    {
        ContextDto contextDto = ContextFinder.GetById(Req.ContextId);
        _getAnyListRepository = new GetAnyListRepository(Mssql.GetInstanceByDto(contextDto));
    }
    
    public void Invoke()
    {
        _getAnyListRepository.Invoke("");
    }
}