using System;
using System.Collections.Generic;
using System.Linq;
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
        int perPage = 1000;
        int numInserts = insValues.Count;
        
        List<Tuple<int, int>> pages = Paginator.GetPages(numInserts, perPage);
        
        List<List<string>> in1000 = new();
        if (pages == null)
            return in1000;

        foreach (Tuple<int, int> range in pages)
        {
            int from = range.Item1;
            int to = range.Item2;
            List<string> batch = insValues.Skip(from).Take(perPage).ToList();
            in1000.Add(batch);
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
            value = value.Replace("'": "''");
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