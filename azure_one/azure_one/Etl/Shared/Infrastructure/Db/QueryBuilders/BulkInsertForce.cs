using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using azure_one.Etl.Shared.Infrastructure.Db;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;


public sealed class BulkInsertForce
{
    private readonly string _targetTable;
    private readonly Dictionary<string, string> _columnMapping;
    private readonly List<Dictionary<string, string>> _dataRows;
    
    public const string TAG_CONSTANT = "constant"; 

    public BulkInsertForce(
        string targetTable, 
        List<Dictionary<string, string>> dataRows
    )
    {
        _targetTable = targetTable;
        _dataRows = dataRows;
    }

    private void LoadColumnMapping()
    {
        Dictionary<string, string> row = _dataRows[0];
        foreach (KeyValuePair<string, string> kv in row)
            if (!_columnMapping.ContainsKey(kv.Key))
                _columnMapping.Add(kv.Key, kv.Key);
    }
    
    public string GetBulkInsertForceQuery()
    {
        LoadColumnMapping();
        List<string> insValues = new List<string>();
        foreach (Dictionary<string, string> dicRow in _dataRows)
        {
            string strvalues = GetValuesBetweenParenthesis(dicRow);
            insValues.Add(strvalues);
        }

        string sql = $"DROP TABLE IF EXISTS {_targetTable};";
        sql += GetCreateTableDQL();
        
        List<List<string>> splitted = Get1000Splitted(insValues);
        foreach (List<string> batchInsVals in splitted)
        {
            sql += GetInsertIntoHeader();
            sql += string.Join(",", batchInsVals);
            sql += ";";
        }
        return sql;
    }

    private string GetCreateTableDQL()
    {
        return (new CreateTableQuery(_targetTable, GetColumnNamesFromMapping())).Invoke();
    }
    
    private List<string> GetColumnNamesFromMapping()
    {
        List<string> columnNames = new();
        foreach (KeyValuePair<string, string> kv in _columnMapping)
            columnNames.Add(kv.Value);
        return columnNames;
    }
    
    private List<List<string>> Get1000Splitted(List<string> insValues)
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
            List<string> batch = insValues.Skip(from).Take(perPage).ToList();
            in1000.Add(batch);
        }
        return in1000;
    }
    
    private string GetInsertIntoHeader()
    {
        List<string> values = new List<string>()
        {
            $"INSERT INTO {_targetTable} (",
            "[" + string.Join("], [",_columnMapping.Values) + "]",
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