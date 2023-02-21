using System.Collections.Generic;
using azure_one.Etl.Infrastructure.Files;
using azure_one.Etl.Infrastructure.Env;
using azure_one.Etl.Infrastructure.Db;
using azure_one.Etl.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.Infrastructure.Log;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadLanguagesRaw
{
    private readonly ExcelReader _excelReader;

    public LoadLanguagesRaw()
    {
        string pathExcel = Env.Get("HOME")+"/Downloads/data-in.xlsx";
        const int sheetNr = 1;
        const int maxColumn = 3;
        _excelReader = ExcelReader.FromPrimitives((pathExcel, sheetNr, maxColumn));
    }

    public void Invoke()
    {
        string sql = (
            new BulkInsert(
                "languages", 
                new Dictionary<string, string>() {
                    { "Column0", "uuid" },
                    { "Column1", "val" },
                    { "Column2", "codesap" },
                }, 
                _excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        
        Lg.Pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}