using System.Collections.Generic;
using azure_one.Etl.Infrastructure.Files;
using azure_one.Etl.Infrastructure.Env;
using azure_one.Etl.Infrastructure.Db;
using azure_one.Etl.Infrastructure.Log;

namespace azure_one.Etl.Application;

public sealed class LoadExcelService
{
    private readonly ExcelReader _excelReader;
    
    private readonly Dictionary<string, string> _mapping = new Dictionary<string, string>()
    {
        {"Column0","uuid"},
        {"Column1","val"},
        {"Column2","codesap"},
    };

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
        
        List<string> insValues = new List<string>();
        foreach (Dictionary<string, string> dicRow in sheetData)
        {
            string strvalues = this.GetValuesBetweenParenthesis(dicRow);
            insValues.Add(strvalues);
        }

        string sql = this.GetInsertIntoHeader("languages");
        sql += string.Join(",", insValues);
        sql += ";";
        Lg.Pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
    
    private string GetValuesBetweenParenthesis(Dictionary<string, string> row)
    {
        List<string> values = new List<string>();
        foreach (var map in this._mapping)
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

    private string GetInsertIntoHeader(string table)
    {
        List<string> values = new List<string>()
        {
            $"INSERT INTO [local_staging].[dbo].[{table}] (",
            string.Join(",",this._mapping.Values),
            ") VALUES"
        };
        return string.Join("", values);
    }
    
}