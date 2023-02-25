using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

namespace azure_one.Etl.Transformers.Infrastructure.Repositories;

public class SqlFilesRepository
{
    private readonly string _pathFilesFolder;
    private readonly Mssql _db;

    public SqlFilesRepository(Mssql db)
    {
        _db = db;        
        //_pathFilesFolder = FileHelper.GetInstance().GetFilePath("/000_imp_tables.sql");
        //_pathFilesFolder = Assembly.GetEntryAssembly().Location;
        //_pathFilesFolder = Path.Combine(Directory.GetCurrentDirectory(), "sql_files/000_imp_tables.sql");
        _pathFilesFolder = FileHelper.GetSqlFilesFolder();
    }
    
    public void RunDemo()
    {
        string[] sqlFiles = FileHelper.GetFileNamesInDir(_pathFilesFolder, ".sql");
        if (sqlFiles.IsEmpty())
        {
            Lg.pr("no files found");
            return;
        }
        
        string pathFile = _pathFilesFolder + "/000_imp_tables.sql";
        Lg.pr(pathFile,"path-file");
        string sql = FileHelper.GetFileContent(pathFile);
        Lg.pr(sql);
        _db.Execute(sql);
    }
}