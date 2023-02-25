using azure_one.Etl.SQLRunners.Infrastructure.Repositories;

namespace azure_one.Etl.SQLRunners.Application;

public sealed class RunSqlFilesService
{
    private readonly SqlFilesRepository _sqlFilesRepository;
    public RunSqlFilesService(SqlFilesRepository sqlFilesRepository)
    {
        _sqlFilesRepository = sqlFilesRepository;
    }

    public void Invoke()
    {
        _sqlFilesRepository.Invoke();
    }
}