using System;
using System.Linq;
using System.Collections.Generic;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class ReadQuery
{
    
    private string _comment = "";
    private string _table = "";
    private bool _isDistinct = false;
    private bool calcFoundRows = false;
    private List<string> _arGetFields = new List<string>();
    private List<string> arJoins = new List<string>();
    private List<string> arAnds = new List<string>();
    private List<string> arGroupBy = new List<string>();
    private List<string> arHaving = new List<string>();
    private Dictionary<string, string> arOrderBy = new Dictionary<string, string>();
    private Dictionary<string, int> arLimit = new Dictionary<string, int>();
    private List<string> arEnd = new List<string>();

    private List<string> arNumeric = new List<string>();
    private Dictionary<string, string> arInsertFv = new Dictionary<string, string>();

    private Dictionary<string, string> arUpdateFv = new Dictionary<string, string>();
    private Dictionary<string, string> arPKs = new Dictionary<string, string>();
    private List<string> _select = new List<string>();

    private string _sql = "";
    private string _sqlCount = "";

    private object oDB = null;

    private List<string> reserved = new List<string> { "get", "order", "password" };

    public const string READ = "r";
    public const string WRITE = "w";

    public ReadQuery(string table)
    {
        _table = table;
    }

    public static ReadQuery fromTable(string table)
    {
        return (new(table));
    }
    
    private string GetJoins()
    {
        if (!arJoins.Any())
            return "";
        return string.Join("\n", arJoins);
    }

    private string GetHaving()
    {
        if (!arHaving.Any())
            return "";
        return "HAVING " + string.Join(", ", arHaving);
    }

    private string GetGroupBy()
    {
        if (!arGroupBy.Any())
            return "";
        return "GROUP BY " + string.Join(",", arGroupBy);
    }

    private string GetOrderBy()
    {
        if (!arOrderBy.Any())
            return "";
        return "ORDER BY " + string.Join(",", arOrderBy.Select(kvp => $"{kvp.Key} {kvp.Value}"));
    }

    private string GetEnd()
    {
        if (!arEnd.Any())
            return "";
        return " " + string.Join("\n", arEnd);
    }

    private string GetLimit()
    {
        if (!arLimit.Any())
            return "";
        if (arLimit["regfrom"] == 0)
            return " LIMIT " + arLimit["perpage"];
        var limit = string.Join(", ", arLimit.Values);
        return " LIMIT " + limit;
    }

    private bool IsNumeric(string fieldName)
    {
        return arNumeric.Contains(fieldName);
    }

    private bool IsReserved(string word)
    {
        return reserved.Contains(word.ToLower());
    }

    private void CleanReserved(object mxFields)
    {
        if (mxFields is string)
        {
            if (IsReserved(mxFields.ToString()))
                mxFields = $"[{mxFields}]";
            return;
        }

        if (mxFields is List<string>)
        {
            List<string> temp = mxFields as List<string>;
            for (var i = 0; i < temp.Count; i++)
            {
                var field = temp[i];
                if (IsReserved(field))
                    temp[i] = $"[{field}]";
            }
            mxFields = temp;
        }
    }

    public ReadQuery AddGetField(string fieldname)
    {
        _arGetFields.Add(fieldname);
        return this;
    }

    public ReadQuery Select()
    {
        _sql = "/*error select*/";
        _sqlCount = "/*error select count*/";
        if (string.IsNullOrEmpty(_table))
             throw new Exception("missing fields in select");
        
        string comment = string.IsNullOrEmpty(_comment) ? "/*select*/" :  $"/*{_comment}*/";
        _select.Add($"{comment} SELECT");
        
        if (_isDistinct) _select.Add("DISTINCT");

        _select.Add(string.Join(",", _arGetFields));
        _select.Add($"FROM [{_table}]");

        _sql = string.Join(" ", _select);
        return this;
    }

    public string GetSql()
    {
        return _sql;
    }

}