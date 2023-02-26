using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class PreLoadImpTablesService: AbsRawService
{
    private readonly PreloadRepository _preloadRepository;
   
    public PreLoadImpTablesService(PreloadRepository preloadRepository)
    {
        _preloadRepository = preloadRepository;
    }
    
    public override void Invoke()
    {
       _preloadRepository.Invoke(); 
    }
}