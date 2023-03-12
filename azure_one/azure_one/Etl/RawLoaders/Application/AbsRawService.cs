using azure_one.Etl.Shared.Infrastructure.Db.Contexts;
using azure_one.Etl.Shared.Infrastructure.Global;

namespace azure_one.Etl.RawLoaders.Application;

public abstract class AbsRawService
{
    public abstract void Invoke();

    protected string ChangeDatabaseByReq(string sql)
    {
        ContextDto contextDto = ContextFinder.GetById(Req.ContextId);
        return sql.Replace("local_staging", contextDto.Database);
    }
}