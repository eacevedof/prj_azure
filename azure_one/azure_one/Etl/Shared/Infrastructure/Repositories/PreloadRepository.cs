using System;
using Microsoft.VisualBasic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Log;

namespace azure_one.Etl.Shared.Infrastructure.Repositories;

public sealed class PreloadRepository: AbsRepository
{
    private readonly string _pathFilesFolder;

    public PreloadRepository(Mssql db) : base(db)
    {
        _pathFilesFolder = FileHelper.GetSqlFilesFolder();
        _pathFilesFolder += "/pre_load";
    }

    public void TruncateTable(string tableName)
    {
        string sql = $"TRUNCATE TABLE [local_staging].[dbo].[{tableName}]";
        Lg.pr(sql);
        _db.Execute(sql);
    }
    
    public void Invoke()
    {
        Lg.pr("Preload SQL files execution started!");
        string[] sqlFiles = FileHelper.GetFileNamesInDir(_pathFilesFolder, "*.sql");
        if (sqlFiles.IsEmpty())
        {
            Lg.pr($"no files found in {_pathFilesFolder}");
            return;
        }
        
        Array.Sort(sqlFiles);
        Lg.pr(string.Join("\n", sqlFiles));
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
            _db.ExecuteRaw(sql);
        }
    }    
}