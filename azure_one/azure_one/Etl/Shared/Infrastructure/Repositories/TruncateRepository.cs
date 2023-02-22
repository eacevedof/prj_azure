using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public sealed class TruncateRepository: AbsRepository
{
    public TruncateRepository(Mssql db) : base(db)
    {
    }
    
}