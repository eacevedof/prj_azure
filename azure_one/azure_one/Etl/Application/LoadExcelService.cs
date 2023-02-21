using System;
using System.Collections.Generic;
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
    
    private Dictionary<string, string> mapping = new Dictionary<string, string>()
    {
        {"Column0","uuid"},
        {"Column1","val"},
        {"Column1","codesap"},
    };

    public LoadExcelService(ExcelReader excelReader)
    {
        _excelReader = excelReader;
        this.PATH_EXCEL = Env.Get("HOME")+"/Downloads/data-in.xlsx";
    }

    public void Invoke()
    {
        var list = this._excelReader.GetData(this.PATH_EXCEL, 2, 4);
        List<string> inParenthesis = new List<string>();
        
        foreach (var row in list)
        {
            string insValues = this.GetValuesBetweenParenthesis(row);
            Lg.Pr(insValues);
            inParenthesis.Add(insValues);
        }
        //string json = JsonSerializer.Serialize(list);
        //Lg.Pr(json, "result");
    }
    
    private string GetValuesBetweenParenthesis(Dictionary<string, string> row)
    {
        List<string> values = new List<string>();
        foreach (var map in this.mapping)
        {
            string column = map.Key;
            //string field = map.Value;
            string value = row.GetValueOrDefault(column);
            value = value.Replace("'", "''");
            values.Add(value);
        }

        string result = string.Join("','",values);
        return $"('{result}')";
    }
    
    
}