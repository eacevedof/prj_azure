using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class PreloadImpTablesService: AbsRawService
{
    private readonly PreloadRepository _preloadRepository;
   
    public PreloadImpTablesService(PreloadRepository preloadRepository)
    {
        _preloadRepository = preloadRepository;
    }
    
    public override void Invoke()
    {
       _preloadRepository.Invoke(); 
    }
}