using System;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Repositories;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.VisualBasic;

namespace azure_one.Etl.SqlRunners.Infrastructure.Repositories;

public sealed class PreLoadRepository: AbsRepository
{
    private readonly string _pathFilesFolder;

    public PreLoadRepository(Mssql db) : base(db)
    {
        _pathFilesFolder = FileHelper.GetSqlFilesFolder();
        _pathFilesFolder += "/pre_load";
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

            sql = GetChangedDatabaseByReq(sql);
            Lg.pr(sql);
            _db.ExecuteRaw(sql);
        }
    }    
}