using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public abstract class AbsRepository
{
    protected Mssql _db;

    protected AbsRepository(Mssql db)
    {
        this._db = db;
    }
}