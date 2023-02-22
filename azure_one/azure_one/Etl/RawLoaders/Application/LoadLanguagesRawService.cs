using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.RawLoaders.Domain.Enums;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadLanguagesRawService: AbsRawService
{
    public override void Invoke()
    {
        string pathExcel = Env.GetConcat("HOME", ExcelSheetsEnum.path_file);
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            ExcelSheetsEnum.languages_sheetnr, 
            ExcelSheetsEnum.languages_max_col
        ));
        
        string sql = (
            new BulkInsert(
                ExcelSheetsEnum.languages_table, 
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