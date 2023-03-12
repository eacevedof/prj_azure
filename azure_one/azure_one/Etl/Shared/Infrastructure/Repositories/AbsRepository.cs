using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Global;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public abstract class AbsRepository
{
    protected Mssql _db;

    protected AbsRepository(Mssql db)
    {
        _db = db;
    }

    protected string GetMssqlSanitized(string value)
    {
        return value.Replace("'", "''");
    }
    
    protected string GetChangedDatabaseByReq(string sql)
    {
        ContextDto contextDto = ContextFinder.GetById(Req.ContextId);
        //sql = sql.Replace("local_laciahub", contextDto.Database);
        return sql.Replace("local_staging", contextDto.Database);
    }
}