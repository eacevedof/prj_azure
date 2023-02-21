using System.Collections.Generic;
using azure_one.Etl.Infrastructure.Files;
using azure_one.Etl.Infrastructure.Env;
using azure_one.Etl.Infrastructure.Db;
using azure_one.Etl.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.Infrastructure.Log;

namespace azure_one.Etl.Application;

public sealed class LoadExcelService
{
    private readonly ExcelReader _excelReader;

    public LoadExcelService()
    {
        string pathExcel = Env.Get("HOME")+"/Downloads/data-in.xlsx";
        const int sheetNr = 1;
        const int maxColumn = 3;
        _excelReader = ExcelReader.FromPrimitives((pathExcel, sheetNr, maxColumn));
    }

    public void Invoke()
    {
        List<Dictionary<string, string>> sheetData = this._excelReader.GetData();
        string sql = (
            new BulkInsert(
                "languages", 
                new Dictionary<string, string>() {
                    { "Column0", "uuid" },
                    { "Column1", "val" },
                    { "Column2", "codesap" },
                }, 
                sheetData)
            ).GetBulkInsertQuery();
        
        Lg.Pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}