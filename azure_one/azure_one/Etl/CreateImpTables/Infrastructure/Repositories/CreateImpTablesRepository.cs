
using System;
using Microsoft.VisualBasic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Repositories;

namespace azure_one.Etl.CreateImpTables.Infrastructure.Repositories;

public sealed class CreateImpTablesRepository: AbsRepository
{
    private readonly string _pathFilesFolder;

    public CreateImpTablesRepository(Mssql db) : base(db)
    {
        _pathFilesFolder = FileHelper.GetSqlFilesFolder();
        _pathFilesFolder += "/ddl";
    }
    
    public void Invoke()
    {
        Lg.pr("CreateImpTablesRepository started!");
        CleanAndCreateTables();
        CreateViews();
        Lg.pr("CreateImpTablesRepository finished!");
    }

    private void CleanAndCreateTables()
    {
        Lg.pr("CleanAndCreateTables started!");
        string pathFolder = _pathFilesFolder + "/create_table";
        string[] sqlFiles = FileHelper.GetFileNamesInDir(pathFolder, "*.sql");
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
        
        Lg.pr("CleanAndCreateTables end!");
    }
    
    private void CreateViews()
    {
        Lg.pr("CreateViews started!");
        string pathFolder = _pathFilesFolder + "/views";
        string[] sqlFiles = FileHelper.GetFileNamesInDir(pathFolder, "*.sql");
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
        Lg.pr("CreateViews end!");
    }    
}