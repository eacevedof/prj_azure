using azure_one.Etl.Shared.Infrastructure.Repositories;
using azure_one.Etl.SqlRunners.Infrastructure.Repositories;

namespace azure_one.Etl.SqlRunners.Application;

public sealed class PreLoadImpTablesService
{
    private readonly PreLoadRepository _preLoadRepository;
   
    public PreLoadImpTablesService(PreLoadRepository preLoadRepository)
    {
        _preLoadRepository = preLoadRepository;
    }
    
    public void Invoke()
    {
       _preLoadRepository.Invoke(); 
    }
}