using azure_one.Etl.Transformers.Infrastructure.Repositories;

namespace azure_one.Etl.Transformers.Application;

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