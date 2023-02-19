using azure_one.Etl.Infrastructure.Db;

namespace azure_one.Etl.Infrastructure.Repositories;

public abstract class AbsRepository
{
    protected Mssql _db;

    protected AbsRepository(Mssql db)
    {
        this._db = db;
    }
}