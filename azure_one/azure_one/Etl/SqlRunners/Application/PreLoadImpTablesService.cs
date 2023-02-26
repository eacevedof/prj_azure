using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.SqlRunners.Application;

public sealed class PreLoadImpTablesService
{
    private readonly PreloadRepository _preloadRepository;
   
    public PreLoadImpTablesService(PreloadRepository preloadRepository)
    {
        _preloadRepository = preloadRepository;
    }
    
    public void Invoke()
    {
       _preloadRepository.Invoke(); 
    }
}