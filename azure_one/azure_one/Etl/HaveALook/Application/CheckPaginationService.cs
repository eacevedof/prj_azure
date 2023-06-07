using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Global;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

using azure_one.Etl.HaveALook.Infrastructure.Repositories;
using azure_one.Etl.HaveALook.Domain;

namespace azure_one.Etl.HaveALook.Application;

public sealed class CheckPaginationService
{
    private readonly GetAnyListRepository _getAnyListRepository;

    public CheckPaginationService()
    {
        ContextDto contextDto = ContextFinder.GetById(Req.ContextId);
        _getAnyListRepository = new GetAnyListRepository(Mssql.GetInstanceByDto(contextDto));
    }
    
    public ProvincesDto Invoke(FilterDto filterDto)
    {
        //int page = int.Parse(Req.Request["page"]);
        //int pageSize = int.Parse(Req.Request["pagesize"]);
        
        return _getAnyListRepository.Invoke(filterDto.page(), filterDto.pageSize());
    }
}