using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;


public sealed class BulkInsert
{
    private readonly string _targetTable;
    private readonly Dictionary<string, string> _columnMapping;
    private readonly List<Dictionary<string, string>> _dataRows;
    
    public const string TAG_CONSTANT = "constant"; 

    public BulkInsert(
        string targetTable, 
        Dictionary<string, string> columnMapping,
        List<Dictionary<string, string>> dataRows)
    {
        _targetTable = targetTable;
        _columnMapping = columnMapping;
        _dataRows = dataRows;
    }
    
    public string GetBulkInsertQuery()
    {
        List<string> insValues = new List<string>();
        foreach (Dictionary<string, string> dicRow in _dataRows)
        {
            string strvalues = GetValuesBetweenParenthesis(dicRow);
            insValues.Add(strvalues);
        }

        List<List<string>> splitted = Get1000Splitted(insValues);

        string sql = "";
        foreach (List<string> batchInsVals in splitted)
        {
            sql += GetInsertIntoHeader();
            sql += string.Join(",", batchInsVals);
            sql += ";";
        }
        return sql;
    }
    
    public List<List<string>> Get1000Splitted(List<string> insValues)
    {
        List<List<string>> in1000 = new();
        int count = 0;
        List<string> tmp = new();
        foreach (string value in insValues)
        {
            tmp.Add(value);
            if ((count % 1000) == 0)
            {
                in1000.Add(tmp);
                count = 0;
                tmp = new();
            }
            count++;
        }
        return in1000;
    }
    
    private string GetInsertIntoHeader()
    {
        List<string> values = new List<string>()
        {
            $"INSERT INTO [local_staging].[dbo].[{_targetTable}] (",
            string.Join(",",_columnMapping.Values),
            ") VALUES"
        };
        return string.Join("", values);
    }        
    
    private string GetValuesBetweenParenthesis(Dictionary<string, string> row)
    {
        List<string> values = new List<string>();
        foreach (KeyValuePair<string,string> columnMap in _columnMapping)
        {
            string column = columnMap.Key.Trim();
            string defValue = GetDefaultValue(column, TAG_CONSTANT);

            string value = row.GetValueOrDefault(column) ?? defValue;
            value = value.Replace("'", "''");
            values.Add(value);
        }

        string result = string.Join("','",values);
        return $"('{result}')";
    }

    private string GetDefaultValue(string columnName, string tag)
    {
        tag = TAG_CONSTANT + ":";
        if (!columnName.Contains(tag)) return "";
        string[] parts = columnName.Split("constant:");
        return parts[1] ?? "";
    }

}