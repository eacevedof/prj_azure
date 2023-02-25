using azure_one.Etl.SQLRunners.Infrastructure.Repositories;

namespace azure_one.Etl.SQLRunners.Application;

public sealed class TransformDemoService
{
    private readonly SqlFilesRepository _sqlFilesRepository;
    public TransformDemoService(SqlFilesRepository sqlFilesRepository)
    {
        _sqlFilesRepository = sqlFilesRepository;
    }

    public void Invoke()
    {
        _sqlFilesRepository.Invoke();
    }
}