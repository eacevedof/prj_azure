using azure_one.Etl.Infrastructure.Db;

namespace azure_one.Etl.Infrastructure.Repositories;

public sealed class ExcelFilesRepository: AbsRepository
{
    public ExcelFilesRepository(Mssql mssql) : base(mssql)
    {
    }
    
}