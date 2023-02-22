using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public sealed class ExcelFilesRepository: AbsRepository
{
    public ExcelFilesRepository(Mssql db) : base(db)
    {
    }
    
}