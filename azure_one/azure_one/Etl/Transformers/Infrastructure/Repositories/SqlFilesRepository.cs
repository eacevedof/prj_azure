using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace azure_one.Etl.Transformers.Infrastructure.Repositories;

public class SqlFilesRepository
{
    private readonly string _pathFilesFolder;
    private readonly Mssql _db;

    public SqlFilesRepository(Mssql db)
    {
        _db = db;        
        //_pathFilesFolder = FileHelper.GetInstance().GetFilePath("/demo.sql");
        _pathFilesFolder = Assembly.GetEntryAssembly().Location;
        _pathFilesFolder = Path.Combine(Directory.GetCurrentDirectory(), "sql_files/demo.sql");
        bool r = File.Exists(_pathFilesFolder);
        string x = "";
    }
    
    public void RunFileDemo()
    {
        return;
        string pathFile = _pathFilesFolder + "/demo.sql";
        Lg.pr(pathFile,"path-file");
        string sql = FileHelper.GetFileContent(pathFile);
        Lg.pr(sql);
        _db.Execute(sql);
    }
}