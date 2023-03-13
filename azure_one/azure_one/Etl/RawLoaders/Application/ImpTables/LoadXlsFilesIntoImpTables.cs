using System;
using System.Collections.Generic;
using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;

namespace azure_one.Etl.RawLoaders.Application.ImpTables;

public sealed class LoadXlsFilesIntoImpTables: AbsRawService
{
    public override void Invoke()
    {
        string pathFolder = FileHelper.GetMappingFolder();
        pathFolder = $"{pathFolder}/imp_tables";
        string[] jsonFiles = FileHelper.GetFileNamesInDir(pathFolder, "*.json");

        foreach (string jsonPath in jsonFiles)
        {
            if (jsonPath.Contains("-- "))
            {
                Lg.pr($"{jsonPath} skipping ...");
                continue;
            }
            LoadJson(jsonPath);
        }
    }

    private void LoadJson(string pathJson)
    {
        Lg.pr($"\nexcel file config {pathJson} started");
        ExcelMapper excelMapper = ExcelMapper.GetInstance(pathJson);
        ExcelReader excelReader = ExcelReader.FromPrimitivesSheetName((
            excelMapper.Source["path"],
            excelMapper.Source["sheet_name"], 
            Int32.Parse(excelMapper.Source["sheet_max_col"])
        ));
        
        string sql = (
            new BulkInsert(
                excelMapper.Target["table"],
                excelMapper.Mapping,
                excelReader.GetData(excelMapper.Mapping)
            )
        ).GetBulkInsertQuery();
        
        sql = GetChangedDatabaseByReq(sql);
        Lg.pr(sql);
        Mssql.GetInstanceByReq().Execute(sql);
        Lg.pr($"excel file config {pathJson} finished!\n");
    }
}