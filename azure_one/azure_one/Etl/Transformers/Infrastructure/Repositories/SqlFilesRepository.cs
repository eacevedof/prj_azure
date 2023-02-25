using System;
using Microsoft.VisualBasic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
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
        _db = db;        
        _pathFilesFolder = FileHelper.GetSqlFilesFolder();
    }
    
    public void Invoke()
    {
        Lg.pr("Run demo :)");
        string[] sqlFiles = FileHelper.GetFileNamesInDir(_pathFilesFolder, "*.sql");
        if (sqlFiles.IsEmpty())
        {
            Lg.pr($"no files found in {_pathFilesFolder}");
            return;
        }
        
        Array.Sort(sqlFiles);
        foreach (string pathFile in sqlFiles)
        {
            Lg.pr($"handling file: {pathFile}");
            string sql = FileHelper.GetFileContent(pathFile);
            sql = Strings.Trim(sql);
            if (sql.IsEmpty())
            {
                Lg.pr($"empty file {pathFile} skipping...");
                continue;
            }
            Lg.pr(sql);
            _db.Execute(sql);
        }
    }
}