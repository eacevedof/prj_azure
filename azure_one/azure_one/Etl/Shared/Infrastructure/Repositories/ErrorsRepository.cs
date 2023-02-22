using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public sealed class ErrorsRepository: AbsRepository
{
    public ErrorsRepository(Mssql db) : base(db) { }

    public void Save(string tableName)
    {

    }
}