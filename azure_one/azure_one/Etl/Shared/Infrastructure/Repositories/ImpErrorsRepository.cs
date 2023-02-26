using azure_one.Etl.Shared.Domain.Repositories;
using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public sealed class ImpErrorsRepository: AbsRepository, ImpErrorsRepositoryInterface
{
    public ImpErrorsRepository(Mssql db) : base(db) { }

    public static ImpErrorsRepository GetInstance()
    {
        return new ImpErrorsRepository(new Mssql());
    }
    
    public void save(string title, string error)
    {
        error = GetMssqlSanitized(error);
        title = GetMssqlSanitized(title);

        string sql = @$"
        INSERT INTO [local_staging].[dbo].[imp_errors](title, reason) 
        VALUES ('{title}','{error}')
        ";
        _db.Execute(sql);
    }

    public static void add(string title, string error)
    {
        GetInstance().save(title, error);
    }
}