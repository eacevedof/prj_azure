using System.Collections.Generic;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class CreateTableQuery
{
    private readonly string _table;
    private readonly string _autoIdField;
    private readonly List<string> _fields;

    public CreateTableQuery(string table, List<string> fields, string autoIdField = "auto_id")
    {
        _table = table;
        _fields = fields;
        _autoIdField = autoIdField;
    }

    public string Invoke()
    {
        List<string> createList = new();
        createList.Add($"CREATE TABLE {_table} ");
        createList.Add("(");

        List<string> fields = new();
        fields.Add($"[{_autoIdField}] INT IDENTITY(1,1) PRIMARY KEY");
        foreach (string field in _fields)
            fields.Add($"[{field}] NVARCHAR(MAX) NULL");
        
        string strfields = string.Join(",\n", fields);
        createList.Add(strfields);
        createList.Add(");");
        return string.Join("\n", createList);
    }
}