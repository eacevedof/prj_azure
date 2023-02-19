using azure_one.Etl.Infrastructure.Db;

namespace azure_one.Etl.Infrastructure.Repositories;

public abstract class AbsRepository
{
    protected Mssql _mssql;

    protected AbsRepository(Mssql mssql)
    {
        this._mssql = mssql;
    }
}