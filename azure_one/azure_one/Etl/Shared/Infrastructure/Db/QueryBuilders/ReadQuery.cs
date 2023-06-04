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
    private List<string> _arJoins = new List<string>();

    private List<string> _arWhere = new List<string>();
    private List<string> _arGroupBy = new List<string>();
    private List<string> _arHaving = new List<string>();
    private Dictionary<string, string> arOrderBy = new Dictionary<string, string>();
    private Dictionary<string, int> arOffset = new Dictionary<string, int>();
    private List<string> arEnd = new List<string>();

    private List<string> arNumeric = new List<string>();

    private List<string> _select = new List<string>();

    private string _sql = "";
    private string _sqlCount = "";

    private object oDB = null;

    private List<string> _reserved = new List<string> { "get", "order", "password" };

    private const string _READ = "r";
    //public const string WRITE = "w";

    public ReadQuery(string table)
    {
        _table = table;
    }

    public static ReadQuery fromTable(string table)
    {
        return (new(table));
    }


    private string _GetFields()
    {
        if (!_arGetFields.Any())
            return "*";
        return string.Join(", ", _arGetFields);
    }


    private string _GetJoins()
    {
        if (!_arJoins.Any())
            return "";
        return string.Join("\n", _arJoins);
    }

    private string _GetWhere()
    {
        string where = "\nWHERE 1=1";
        if (!_arWhere.Any())
            return where;

        return $"{where} " + string.Join("\n", _arWhere);
    }

    private string _GetGroupBy()
    {
        if (!_arGroupBy.Any()) return "";
        return "GROUP BY " + string.Join(", ", _arGroupBy);
    }

    private string _GetHaving()
    {
        if (!_arHaving.Any()) return "";
        return "HAVING " + string.Join(", ", _arHaving);
    }

    private string _GetOrderBy()
    {
        if (!arOrderBy.Any()) return "";
        return "ORDER BY " + string.Join(", ", arOrderBy.Select(kvp => $"{kvp.Key} {kvp.Value}"));
    }

    private string _GetEnd()
    {
        if (!arEnd.Any()) return "";
        return " " + string.Join("\n", arEnd);
    }

    private string _GetLimit()
    {
        if (!arOffset.Any())
            return "";

        if (arOffset["regfrom"] == 0)
            return " LIMIT " + arOffset["perpage"];

        var limit = string.Join(", ", arOffset.Values);
        return " LIMIT " + limit;
    }

    private bool IsNumeric(string fieldName)
    {
        return arNumeric.Contains(fieldName);
    }

    private bool IsReserved(string word)
    {
        return _reserved.Contains(word.ToLower());
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


    public ReadQuery SetComment(string comment)
    {
        _comment = comment;
        return this;
    }


    public ReadQuery AddGetField(string fieldname)
    {
        _arGetFields.Add(fieldname);
        return this;
    }

    public ReadQuery AddJoin(string join)
    {
        _arJoins.Add(join);
        return this;
    }

    public ReadQuery AddWhere(string where)
    {
        _arWhere.Add(where);
        return this;
    }

    public ReadQuery AddGroupBy(string groupBy)
    {
        _arGroupBy.Add(groupBy);
        return this;
    }



    public ReadQuery Select()
    {
        _sql = "/*error select*/";
        _sqlCount = "/*error select count*/";
        if (string.IsNullOrEmpty(_table))
             throw new Exception("missing table in select");

        // forma SELECT DISTINCT ...
        string comment = string.IsNullOrEmpty(_comment) ? "/*select*/" :  $"/*{_comment}*/";
        _select.Add($"{comment} SELECT");
        if (_isDistinct) _select.Add("DISTINCT");

        _select.Add(_GetFields());
        _select.Add($"FROM [{_table}]");
        _select.Add(_GetJoins());
        _select.Add(_GetWhere());
        _select.Add(_GetGroupBy());
        _sql = string.Join(" ", _select);
        return this;
    }

    public string GetSql()
    {
        return _sql;
    }

}