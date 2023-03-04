using System.Collections.Generic;
using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;
using Newtonsoft.Json.Linq;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsCitiesService: AbsRawService
{
    public override void Invoke()
    {
        ImpCitiesEntity citiesEntity = ImpCitiesEntity.GetInstance();

        string pathHome = Env.Get("HOME");
        dynamic config = MappingReader.JsonDecode("cities");
        string pathExcel = config.source.path;
        
        pathExcel = pathExcel.Replace("%folder_in%", "Downloads");
        pathExcel = $"{pathHome}/{pathExcel}";

        string sheetName = config.source.sheet_name ?? "";
        int maxColPosition = config.source.sheet_max_col ?? 5;
        string table = config.target.table;
        Dictionary<string, string> mapping = MappingReader.GetMappingFromObject(config.mapping);

        ExcelReader excelReader = ExcelReader.FromPrimitivesSheetName((
            pathExcel, 
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