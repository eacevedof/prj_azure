using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Log;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public sealed class TruncateRepository: AbsRepository
{
    public TruncateRepository(Mssql db) : base(db) { }

    public void TruncateTable(string tableName)
    {
        string sql = $"TRUNCATE TABLE [local_staging].[dbo].[{tableName}]";
        Lg.pr(sql);
        _db.Execute(sql);
    }
}