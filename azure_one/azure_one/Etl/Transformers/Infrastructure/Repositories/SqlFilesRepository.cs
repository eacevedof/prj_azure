using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;

namespace azure_one.Etl.Transformers.Infrastructure.Repositories;

public class SqlFilesRepository
{
    private readonly string _pathFilesFolder;
    private readonly Mssql _db;

    public SqlFilesRepository(Mssql db)
    {
        _pathFilesFolder = FileHelper.GetFilePath("/Etl/Transformers/Infrastructure/Files");
        _db = db;
    }
    
    public void RunFileDemo()
    {
        string pathFile = _pathFilesFolder + "/demo.sql";
        Lg.pr(pathFile,"path-file");
        string sql = FileHelper.GetFileContent(pathFile);
        Lg.pr(sql);
        _db.Execute(sql);
    }
}