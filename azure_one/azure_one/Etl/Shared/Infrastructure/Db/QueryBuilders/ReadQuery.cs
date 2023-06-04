using System;
using System.Linq;
using System.Collections.Generic;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class ReadQuery
{
    
    private string _comment = "";
    private string _table = "";
    private bool _isDistinct = false;
    private List<string> _arGetFields = new List<string>();
    private List<string> _arJoins = new List<string>();
    private List<string> _arWhere = new List<string>();
    private List<string> _arGroupBy = new List<string>();
    private List<string> _arHaving = new List<string>();
    private Dictionary<string, string> _arOrderBy = new Dictionary<string, string>();
    private Dictionary<string, int> _arOffset = new Dictionary<string, int>();

    private List<string> _arNumeric = new List<string>();
    private List<string> _select = new List<string>();

    private string _sql = "";
    private string _sqlCount = "";

    private object oDB = null;

    private List<string> _reserved = new List<string> { "get", "order", "password" };

    private const string _READ = "r";
    //public const string WRITE = "w";

    public const string ORDER_ASC = "ASC";
    public const string ORDER_DESC = "DESC";

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
        if (!_arOrderBy.Any()) return "";
        return "ORDER BY " + string.Join(", ", _arOrderBy.Select(kvp => $"{kvp.Key} {kvp.Value}"));
    }

    private string _GetLimit()
    {
        if (!_arOffset.Any()) return "";

        if (!_arOrderBy.Any())
            throw new Exception("for pagination is order by field required");

        if (!_arOffset.ContainsKey("reqfrom")) _arOffset.Add("reqfrom", 0);
        if (!_arOffset.ContainsKey("pagesize")) _arOffset.Add("pagesize", 50);

        if (_arOffset["regfrom"] == 0)
            return "\n OFFSET 0 FETCH " + _arOffset["pagesize"] + " ROWS ONLY";

        string strFrom = _arOffset["reqfrom"].ToString();
        return $"\n OFFSET {strFrom} FETCH " + _arOffset["pagesize"] + " ROWS ONLY";
    }

    private bool _IsNumeric(string fieldName)
    {
        return _arNumeric.Contains(fieldName);
    }

    private bool _IsReserved(string word)
    {
        return _reserved.Contains(word.ToLower());
    }

    private void _CleanReserved(object mxFields)
    {
        if (mxFields is string)
        {
            if (_IsReserved(mxFields.ToString()))
                mxFields = $"[{mxFields}]";
            return;
        }

        if (mxFields is List<string>)
        {
            List<string> temp = mxFields as List<string>;
            for (var i = 0; i < temp.Count; i++)
            {
                var field = temp[i];
                if (_IsReserved(field))
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

    public ReadQuery Distinct()
    {
        _isDistinct = true;
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

    public ReadQuery AddHaving(string having)
    {
        _arHaving.Add(having);
        return this;
    }

    public ReadQuery AddOrderBy(string field, string order=ORDER_ASC)
    {
        _arOrderBy.Add(field, order);
        return this;
    }

    public ReadQuery setOffset(int start=0, int pageSize = 25)
    {
        _arOffset = new Dictionary<string, int>();
        _arOffset.Add("regfrom", start);
        _arOffset.Add("pagesize", pageSize);
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
        _select.Add(_GetHaving());

        _sqlCount = string.Join(" ", _select);
        _sqlCount = @$"
        SELECT COUNT(*) rows
        FROM (
            {_sqlCount}
        ) t
        ";

        _select.Add(_GetOrderBy());
        _select.Add(_GetLimit());
        _sql = string.Join(" ", _select);
        return this;
    }

    public string GetSql()
    {
        return _sql;
    }

    public string GetSqlCount()
    {
        return _sqlCount;
    }

}