using azure_one.Etl.SqlRunners.Infrastructure.Repositories;

namespace azure_one.Etl.SqlRunners.Application;

public sealed class PostLoadImpTablesService
{
    private readonly PostLoadRepository _postLoadRepository;
    
    public PostLoadImpTablesService(PostLoadRepository postLoadRepository)
    {
        _postLoadRepository = postLoadRepository;
    }

    public void Invoke()
    {
        _postLoadRepository.Invoke();
    }
}