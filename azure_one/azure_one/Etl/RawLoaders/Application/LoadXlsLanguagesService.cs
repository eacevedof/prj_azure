using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.RawLoaders.Domain.Enums;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsLanguagesService: AbsRawService
{
    public override void Invoke()
    {
        string pathExcel = Env.GetConcat("HOME", ExcelToStagingConfigEnum.path_file);
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            ExcelToStagingConfigEnum.languages_sheetnr, 
            ExcelToStagingConfigEnum.languages_max_col
        ));
        
        string sql = (
            new BulkInsert(
                ExcelToStagingConfigEnum.languages_table, 
                new Dictionary<string, string>() {
                    { "Column0", "uuid" },
                    { "Column1", "val" },
                    { "Column2", "codesap" },
                }, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}