using System.Collections.Generic;
using azure_one.Etl.Infrastructure.Files;
using azure_one.Etl.Infrastructure.Env;
using azure_one.Etl.Infrastructure.Log;
using azure_one.Etl.Infrastructure.Db;
using azure_one.Etl.Infrastructure.Db.QueryBuilders;
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
                "languages", 
                new Dictionary<string, string>() {
                    { "Column0", "uuid" },
                    { "Column1", "val" },
                    { "Column2", "codesap" },
                }, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.Pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}