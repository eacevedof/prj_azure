using System.Collections.Generic;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class BulkInsert
{
    private readonly string _targetTable;
    private readonly Dictionary<string, string> _columnMapping;
    private readonly List<Dictionary<string, string>> _dataRows;

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
            string strvalues = this.GetValuesBetweenParenthesis(dicRow);
            insValues.Add(strvalues);
        }

        string sql = this.GetInsertIntoHeader();
        sql += string.Join(",", insValues);
        sql += ";";
        return sql;
    }
    
    private string GetValuesBetweenParenthesis(Dictionary<string, string> row)
    {
        List<string> values = new List<string>();
        foreach (KeyValuePair<string,string> columnMap in this._columnMapping)
        {
            string column = columnMap.Key;
            string value = row.GetValueOrDefault(column);
            value = value.Replace("'", "''");
            values.Add(value);
        }

        string result = string.Join("','",values);
        return $"('{result}')";
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
}