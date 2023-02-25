using azure_one.Etl.SqlRunners.Infrastructure.Repositories;

namespace azure_one.Etl.SqlRunners.Application;

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