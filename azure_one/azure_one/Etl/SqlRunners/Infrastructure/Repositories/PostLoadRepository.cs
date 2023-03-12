using System;
using Microsoft.VisualBasic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;
using azure_one.Etl.Shared.Infrastructure.Global;

namespace azure_one.Etl.SqlRunners.Infrastructure.Repositories;

public class PostLoadRepository
{
    private readonly string _pathFilesFolder;
    private readonly Mssql _db;

    public PostLoadRepository(Mssql db)
    {
        _db = db;        
        _pathFilesFolder = FileHelper.GetSqlFilesFolder();
        _pathFilesFolder += "/post_load";
    }
    
    public void Invoke()
    {
        Lg.pr("PostLoadRepository started!");
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
        Lg.pr("PostLoadRepository finished!");
    }
    
    private string GetChangedDatabaseByReq(string sql)
    {
        ContextDto contextDto = ContextFinder.GetById(Req.ContextId);
        //sql = sql.Replace("local_laciahub", contextDto.Database);
        return sql.Replace("local_staging", contextDto.Database);
    }
}