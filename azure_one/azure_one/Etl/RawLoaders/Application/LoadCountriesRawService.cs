using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.RawLoaders.Domain.Enums;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadCountriesRawServices: AbsRawService
{
    public override void Invoke()
    {
        string pathExcel = Env.GetConcat("HOME", ExcelSheetsEnum.path_file);
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            ExcelSheetsEnum.countries_sheetnr, 
            ExcelSheetsEnum.countries_max_col
        ));
        
        string sql = (
            new BulkInsert(
                ExcelSheetsEnum.countries_table, 
                new Dictionary<string, string>() {
                    { "Column0", "uuid" },
                    { "Column1", "val" },
                    { "Column2", "codesap" },
                    { "Column3", "tr_1" },
                    { "Column4", "tr_2" },
                    { "Column5", "tr_3" },
                    { "Column6", "tr_4" },
                    { "Column7", "tr_5" },
                    { "Column8", "tr_6" },
                    { "Column9", "tr_7" },
                    { "Column10", "tr_8" },
                    { "Column11", "tr_9" },
                }, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.Pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}