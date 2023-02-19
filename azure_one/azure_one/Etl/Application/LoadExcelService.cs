using System;
using System.Linq;
using azure_one.Etl.Infrastructure.Files;
using azure_one.Etl.Infrastructure.Env;
using System.Text.Json;
using azure_one.Etl.Infrastructure.Log;

namespace azure_one.Etl.Application;


public sealed class LoadExcelService
{
    private string PATH_EXCEL = "";
    private readonly ExcelReader _excelReader;
    
    public LoadExcelService(ExcelReader excelReader)
    {
        _excelReader = excelReader;
        this.PATH_EXCEL = Env.Get("HOME")+"/Downloads/attributes_v2.xlsx";
    }

    public void Invoke()
    {
        var r = this._excelReader.GetData(this.PATH_EXCEL);
        string json = JsonSerializer.Serialize(r);
        Lg.Pr(json, "result");
    }
    
}