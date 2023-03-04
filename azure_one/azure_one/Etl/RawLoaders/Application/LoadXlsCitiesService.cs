using System;
using System.Collections.Generic;
using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsCitiesService: AbsRawService
{
    public override void Invoke()
    {
        string pathHome = Env.Get("HOME");

        ExcelMapper excelMapper = ExcelMapper.GetInstance("cities");

        string sheetName = excelMapper.Source["sheet_name"];
        int maxColPosition = Int32.Parse(excelMapper.Source["max_column"]);
        string table = excelMapper.Target["table"];
        Dictionary<string, string> mapping = MappingReader.GetMappingFromObject(excelMapper.Mapping);

        ExcelReader excelReader = ExcelReader.FromPrimitivesSheetName((
            excelMapper.Source["path"],
            sheetName, 
            maxColPosition
        ));
        
        string sql = (
            new BulkInsert(
                table,
                mapping,
                excelReader.GetData(mapping)
            )
        ).GetBulkInsertQuery();
        
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}